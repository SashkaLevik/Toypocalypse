using Assets.Scripts.Data;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.RoutEvents;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class BattleSystem : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private StagePrizeCalculator _prizeCalculator;
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private Button _completeStage;

        private int _stageNumber;
        private Toy _player;
        private BaseEnemy _enemy;
        private EnemyHealth _enemyHealth;
        private EnemyAI _enemyAI;
        private SkillPanel _skillPanel;
        private RoutMap _routMap;
        private ISaveLoadService _saveLoadService;
        private RoutEvent _currentEvent;
        private PlayerMovement _playerMovement;
        public Toy Player => _player;

        public event UnityAction StageCompleted;

        private void Awake()
            => _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void Start()
        {            
            _player = _playerSpawner.GetComponentInChildren<Toy>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _skillPanel = _player.SkillPanel;
            _skillPanel.SkillPlayed += EnemyTurn;
            _enemySpawner.EnemySpawned += GetEnemyStats;
            _routMap.StageButtonPressed += EnterStage;
            _routMap.EventEntered += SetEvent;
            _completeStage.onClick.AddListener(CompleteStage);
            OpenNextStage();
        }        

        private void OnDestroy()
        {
            _enemySpawner.EnemySpawned -= GetEnemyStats;
            _completeStage.onClick.RemoveListener(CompleteStage);
        }

        public void Construct(PlayerSpawnPoint playerSpawner, EnemySpawner enemySpawner, RoutMap routMap)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _routMap = routMap;
        }        
        
        public void AnsigneEvents()//вызвать на событие завершения уровня
        {
            _routMap.StageButtonPressed -= EnterStage;
            _routMap.EventEntered -= SetEvent;
            _skillPanel.SkillPlayed -= EnemyTurn;
        }

        private void SetEvent(RoutEvent routEvent)
        {
            _currentEvent = routEvent;
            _currentEvent.EventCompleted += OpenNextStage;
            _playerMovement.SetDefoultPosition();
            _skillPanel.ResetMoveButtons();
        }

        private void EnterStage()
            => _enemySpawner.SpawnEnemy(_stageNumber);

        private void CompleteStage()
        {
            StageCompleted?.Invoke();
            _routMap.gameObject.SetActive(true);
            _routMap.OpenNextEvents(_stageNumber);            
            _stageNumber++;
            _prizeCalculator.DisableBox();
            _winWindow.SetActive(false);
            _saveLoadService.SaveProgress();
        }

        private void OpenNextStage()
        {
            _routMap.OpenStage(_stageNumber);
            _saveLoadService.SaveProgress();
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
            _enemyAI.ChooseAction();
        }        
        
        private void OnEnemyDie()
        {
            _winWindow.SetActive(true);
            _prizeCalculator.GetBox();            
        }

        public void Save(PlayerProgress progress)
        {
            progress.WorldData.Stage = _stageNumber;
        }

        public void Load(PlayerProgress progress)
        {
            _stageNumber = progress.WorldData.Stage;
        }
    }
}
