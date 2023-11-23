using Assets.Scripts.GameFactory;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Toys;
using System;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class BattleState : IPayloadedState1<string, Toy>
    {
        private const string ToySpawnTag = "ToySpawnTag";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private Toy _battleToy;

        public BattleState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, onLoaded: OnSceneLoad);
        }

        public void Enter(string sceneName, Toy toy)
        {
            _loadingCurtain.Show();
            _battleToy = toy;
            _sceneLoader.Load(sceneName, onLoaded: OnSceneLoad);
        }

        public void Exit()
            => _loadingCurtain.Hide();

        private void OnSceneLoad()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<LoopState>();
        }

        private void InitGameWorld()
        {
            CreateToy(_battleToy);
            GameObject battleHud = _gameFactory.CreateBattleHud();
        }

        private void CreateToy(Toy toy)
        {
            _gameFactory.CreateToy(toy, GameObject.FindWithTag(ToySpawnTag));
        }

        private void InformProgressReaders()
        {
            foreach (IloadProgress progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);            
        }        
    }
}
