using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string Menu = "Menu";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }        

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterState);
        }        

        public void Exit() { }        

        private void RegisterServices()
        {
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        }

        private void EnterState()
        {
            _stateMachine.Enter<MenuState, string>(Menu);
        }
    }
}
