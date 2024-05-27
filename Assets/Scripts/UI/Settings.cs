using Assets.Scripts.Data;
using Assets.Scripts.GameEnvironment.Battle;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Settings : MonoBehaviour, ISaveProgress
    {
        private const string MenuScene = "Menu";

        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _close;
        [SerializeField] private Button _returnToMenu;
        [SerializeField] private GameObject _settingsWindow;

        private PlayerSpawnPoint _playerSpawner;
        private Toy _player;
        private PlayerHealth _playerHealth;
        private PlayerSpeed _playerSpeed;
        private ISaveLoadService _saveLoadService;
        private IGameStateMachine _stateMachine;
        private Scene _scene;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _scene = SceneManager.GetActiveScene();
        }

        private void Start()
        {            
            _player = _playerSpawner.GetComponentInChildren<Toy>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerSpeed = _player.GetComponent<PlayerSpeed>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _settingsWindow.SetActive(true);
            }
        }

        private void OnEnable()
        {
            _settings.onClick.AddListener(OpenSettings);
            _close.onClick.AddListener(CloseWindow);
            _returnToMenu.onClick.AddListener(ExitBattle);
        }

        public void Construct(PlayerSpawnPoint playerSpawner)
        {
            _playerSpawner = playerSpawner;
        }            

        private void ExitBattle()
        {
            _saveLoadService.SaveProgress();
            _battleSystem.AnsigneEvents();
            _stateMachine.Enter<MenuState, string>(MenuScene);
        }

        private void CloseWindow()
            => _settingsWindow.SetActive(false);
        

        private void OpenSettings()
        {
            _settingsWindow.SetActive(true);
        }

        public void Save(PlayerProgress progress)
        {
            progress.WorldData.Level = _scene.name;
            progress.PlayerStats.CurrentHP = _playerHealth.CurrentHP;
            progress.PlayerStats.MaxHP = _playerHealth.MaxHP;
            progress.PlayerStats.MaxSpeed = _playerSpeed.MaxSpeed;
        }

        public void Load(PlayerProgress progress)
        {
        }
    }
}
