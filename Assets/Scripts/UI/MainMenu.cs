using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private Button _treeHouseButton;
        [SerializeField] private Button _continue;
        [SerializeField] private GameObject _treeHouseWindow;
        [SerializeField] private GameObject _menu;

        private PlayerProgress _playerProgress;
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void Start()
        {
            if (_playerProgress.WorldData.Level != null)
            {

            }
        }

        private void OnEnable()
        {
            _treeHouseButton.onClick.AddListener(EnterTreeHouse);
            _continue.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            _stateMachine.Enter<LevelState, string>(_playerProgress.WorldData.Level, _playerProgress.PlayerStats.CurrentToy);
        }

        private void EnterTreeHouse()
        {
            _treeHouseWindow.SetActive(true);
            _menu.SetActive(false);
        }

        public void Save(PlayerProgress progress)
        {
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }
    }
}
