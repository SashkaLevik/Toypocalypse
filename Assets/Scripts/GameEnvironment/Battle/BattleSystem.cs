using Assets.Scripts.Data;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.RoutEvents;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class BattleSystem : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private WinStageWindow _windowPrefab;

        private int _stageNumber;
        private Toy _player;
        private BaseEnemy _enemy;
        private EnemyHealth _enemyHealth;
        private EnemyAI _enemyAI;
        private SkillPanel _skillPanel;
        private RoutMap _routMap;
        private ISaveLoadService _saveLoadService;
        private WinStageWindow _winWindow;
        private RoutEvent _currentEvent;
        private PlayerMovement _playerMovement;
        private PlayerMoney _playerMoney;

        private void Awake()
            => _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void Start()
        {            
            _player = _playerSpawner.GetComponentInChildren<Toy>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _playerMoney = _player.GetComponent<PlayerMoney>();
            _skillPanel = _player.SkillPanel;
            _skillPanel.SkillPlayed += EnemyTurn;
            _enemySpawner.EnemySpawned += GetEnemyStats;
            _routMap.StageButtonPressed += EnterStage;
            _routMap.EventEntered += SetEvent;
            OpenNextStage();
        }        

        private void OnDestroy()
        {
            _enemySpawner.EnemySpawned -= GetEnemyStats;
        }

        public void Construct(PlayerSpawnPoint playerSpawner, EnemySpawner enemySpawner, RoutMap routMap)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _routMap = routMap;
        }

        public void Save(PlayerProgress progress)
        {
            progress.WorldData.Stage = _stageNumber;
        }

        public void Load(PlayerProgress progress)
        {
            _stageNumber = progress.WorldData.Stage;
        }
        
        public void AnsigneEvents()//вызвать на событие завершения уровня
        {
            _routMap.StageButtonPressed -= EnterStage;
            _routMap.EventEntered -= SetEvent;
            _skillPanel.SkillPlayed -= EnemyTurn;
            _windowPrefab.StageCompleted -= CompleteStage;
        }

        private void SetEvent(RoutEvent routEvent)
        {
            _currentEvent = routEvent;
            _currentEvent.EventCompleted += OpenNextStage;
            _playerMovement.SetDefoultPosition();
            _skillPanel.ResetMoveButtons();
        }

        private void EnterStage()
        {
            _stageNumber++;
            _enemySpawner.SpawnEnemy(_stageNumber);
        }

        private void CompleteStage()
        {
            _saveLoadService.SaveProgress();
            _routMap.gameObject.SetActive(true);
            _routMap.OpenNextEvents(_stageNumber);
            _playerMoney.AddMaterialByAmount(_winWindow.Gum, _winWindow.Plasticine, _winWindow.Glue, _winWindow.Screw);
        }

        private void OpenNextStage()
        {
            _routMap.OpenStage(_stageNumber);
        }

        private void GetEnemyStats(BaseEnemy enemy)
        {
            _enemy = enemy;
            _player.InitEnemy(_enemy);
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyHealth.Died += OnEnemyDie;
        }    

        private void EnemyTurn()
        {
            //_enemyAI.ChooseAction();
            Debug.Log("EnemyTurn");
        }        
        
        private void OnEnemyDie()
        {
            _winWindow = Instantiate(_windowPrefab);
            _winWindow.StageCompleted += CompleteStage;
        }        
    }
}
