using Assets.Scripts.GameFactory;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using System;

namespace Assets.Scripts.States
{
    public class MenuState : IPayloadedState<string>, IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public MenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }        

        public void Enter() { }        

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            //InitGameWorld();
            //InformProgressReaders();

            _gameStateMachine.Enter<LoopState>();
        }

        private void InformProgressReaders()
        {
            throw new NotImplementedException();
        }

        private void InitGameWorld()
        {
            throw new NotImplementedException();
        }
    }
}