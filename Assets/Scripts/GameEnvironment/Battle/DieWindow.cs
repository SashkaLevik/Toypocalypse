using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEnvironment.Battle
{
    public class DieWindow : MonoBehaviour
    {
        private const string MenuScene = "Menu";

        [SerializeField] private Button _returnMenu;
        [SerializeField] private BattleSystem _battleSystem;

        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
        {
            _returnMenu.onClick.AddListener(ReturnToMenu);
        }

        private void ReturnToMenu()
        {
            _battleSystem.AnsigneEvents();
            _stateMachine.Enter<MenuState, string>(MenuScene);
        }        
    }
}
