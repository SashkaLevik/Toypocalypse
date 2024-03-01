using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class WinLevelWindow : MonoBehaviour, ISaveProgress
    {
        private const string MenuScene = "Menu";

        [SerializeField] private BattleSystem _battleSystem;
        [SerializeField] private Button _returnMenu;

        private PlayerProgress _playerProgress;
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
        {
            _returnMenu.onClick.AddListener(EndGame);
        }

        private void EndGame()
        {
            _playerProgress.IsPlayerCreated = false;
            _playerProgress.WorldData.Stage = 1;
            _saveLoadService.SaveProgress();
            _battleSystem.AnsigneEvents();
            _stateMachine.Enter<MenuState, string>(MenuScene);
        }

        public void Load(PlayerProgress progress)
        {
            _playerProgress = progress;
        }

        public void Save(PlayerProgress progress)
        {
        }
    }
}
