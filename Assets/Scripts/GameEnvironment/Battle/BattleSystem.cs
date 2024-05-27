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
        [SerializeField] private BattleTutorial _battleTutorial;
        [SerializeField] private PlayerSpawnPoint _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private StagePrizeCalculator _prizeCalculator;
        [SerializeField] private GameObject _winWindow;
        [SerializeField] private GameObject _dieWindow;
        [SerializeField] private Button _completeStage;
        [SerializeField] private GameObject _winLvlWindow;

        private int _stageNumber;
        private int _maxStages;
        private Toy _player;
        private PlayerSpeed _playerSpeed;
        private PlayerHealth _playerHealth;
        private BaseEnemy _enemy;
        private EnemyHealth _enemyHealth;
        private EnemyAI _enemyAI;
        private SkillPanel _skillPanel;
        private RoutMap _routMap;
        private ISaveLoadService _saveLoadService;
        private RoutEvent _currentEvent;
        private PlayerMovement _playerMovement;
        private ArtifactsContainer _artifactsContainer;
        private PlayerProgress _playerProgress;       

        public Toy Player => _player;
        public BaseEnemy Enemy => _enemy;
        public ArtifactsContainer ArtifactsContainer => _artifactsContainer;

        public event UnityAction StageCompleted;
        public event UnityAction<bool> BattleEntered;

        private void Awake()
            => _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void Start()
        {
            _player = _playerSpawner.GetComponentInChildren<Toy>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerHealth.Died += OnPlayerDie;
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _skillPanel = _player.SkillPanel;
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
            _skillPanel.PlayerTurnEnded += EnemyTurn;
            _enemySpawner.EnemySpawned += OnEnemySpawned;
            _routMap.StageButtonPressed += EnterStage;
            _routMap.EventEntered += SetEvent;
            _completeStage.onClick.AddListener(CompleteStage);
            _artifactsContainer = _skillPanel.GetComponent<ArtifactsContainer>();
            _enemySpawner.EnemySpawned += _battleTutorial.OpenTutorial;
            OpenNextStage();
        }        

        private void OnDestroy()
        {
            _enemySpawner.EnemySpawned -= OnEnemySpawned;
            _enemySpawner.EnemySpawned -= _battleTutorial.OpenTutorial;
            _completeStage.onClick.RemoveListener(CompleteStage);
        }

        public void Construct(PlayerSpawnPoint playerSpawner, EnemySpawner enemySpawner, RoutMap routMap)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _routMap = routMap;
        }

        public void OnLootBoxOpened()
            => _completeStage.gameObject.SetActive(true);

        public void AnsigneEvents()
        {
            _routMap.StageButtonPressed -= EnterStage;
            _routMap.EventEntered -= SetEvent;            
            //if (_currentEvent != null) _currentEvent.EventCompleted -= SaveGame;
        }        

        private void SetEvent(RoutEvent routEvent)
        {
            _currentEvent = routEvent;
            _currentEvent.EventCompleted += OpenNextStage;
            _currentEvent.EventCompleted += SaveGame;
            _playerMovement.SetDefoultPosition();
        }

        private void SaveGame()
            => _saveLoadService.SaveProgress();

        private void EnterStage()
        {
            BattleEntered?.Invoke(true);
            _enemySpawner.SpawnEnemy(_stageNumber);
        }        

        private void CompleteStage()
        {
            _completeStage.gameObject.SetActive(false);
            BattleEntered?.Invoke(false);
            StageCompleted?.Invoke();
            _routMap.gameObject.SetActive(true);
            _routMap.OpenNextEvents(_stageNumber);            
            _stageNumber++;
            _prizeCalculator.DisableBox();
            _winWindow.SetActive(false);
            PlayerTurn();
            _saveLoadService.SaveProgress();
            //OpenNextStage();
            if (_stageNumber > _maxStages) _winLvlWindow.SetActive(true);
        }

        private void OpenNextStage()
        {
            _routMap.OpenStage(_stageNumber);
            _playerProgress.WorldData.Stage = _stageNumber;

            if (_currentEvent != null)
            {
                _currentEvent.EventCompleted -= OpenNextStage;
                _currentEvent.EventCompleted -= SaveGame;
            }
        }

        private void OnEnemySpawned(BaseEnemy enemy)
        {
            _enemy = enemy;
            _player.InitEnemy(_enemy);
            _skillPanel.GetComponentInChildren<MinionSlot>().InitEnemy(_enemy);
            _enemyAI = _enemy.GetComponent<EnemyAI>();
            _enemyAI.PrepareSkills();
            _enemyHealth = _enemy.GetComponent<EnemyHealth>();
            _enemyAI.EnemyTurnEnded += PlayerTurn;
            _enemyHealth.Died += OnEnemyDie;
        }

        private void PlayerTurn()
        {
            _skillPanel.ResetArea();
            _skillPanel.ResetCooldown();
            _skillPanel.Activate();
            _playerSpeed.ResetAP();
        }

        private void EnemyTurn()
        {
            _enemyAI.EnemyTurn();            
        }

        private void OnEnemyDie()
        {
            _winWindow.SetActive(true);
            _skillPanel.Disactivate();
            _prizeCalculator.GetBox();
            //PlayerTurn();            
            _enemyAI.EnemyTurnEnded -= PlayerTurn;
            _enemyHealth.Died -= OnEnemyDie;
        }

        private void OnPlayerDie()
        {
            _dieWindow.SetActive(true);
            _playerProgress.IsPlayerCreated = false;            

            foreach (var skill in _playerProgress.PlayerSkills)
            {
                if (skill != null)
                    skill.ResetSkill();
            }            

            _playerHealth.Died -= OnPlayerDie;
            _saveLoadService.SaveProgress();
        }

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _stageNumber = progress.WorldData.Stage;
            _playerProgress = progress;
            _maxStages = progress.WorldData.FirstLvlStages;
        }
    }
}
