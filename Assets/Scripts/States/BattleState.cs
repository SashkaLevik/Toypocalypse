using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Player;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.UI;

namespace Assets.Scripts.States
{
    public class BattleState : IPayloadedState1<string, ToyStaticData>, IState
    {
        private const string PlayerSpawnTag = "PlayerSpawnTag";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IToyDataService _toyDataService;
        private Toy _battleToy;
        private ToyStaticData _toyData;

        public BattleState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService, IToyDataService toyDataService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _toyDataService = toyDataService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName);//, onLoaded: OnSceneLoad);
        }

        public void Enter(string sceneName, ToyStaticData toyData)
        {
            _loadingCurtain.Show();
            _toyData = toyData;
            _sceneLoader.Load(sceneName, onLoaded: OnSceneLoad);
        }

        public void Exit()
            => _loadingCurtain.Hide();

        private void OnSceneLoad()
        {
            Debug.Log(_toyData);
            InitGameWorld();
            //InformProgressReaders();
            _stateMachine.Enter<LoopState>();
        }

        private void InitGameWorld()
        {
            GameObject player = _gameFactory.CreateToy(_toyData, GameObject.FindWithTag(PlayerSpawnTag));
            GameObject battleHud = _gameFactory.CreateBattleHud();
            InitBattleHud(player, battleHud);
        }

        private void CreateToy()
        {            
            _gameFactory.CreateToy(_toyData, GameObject.FindWithTag(PlayerSpawnTag));
        }

        private void InitBattleHud(GameObject player, GameObject battleHud)
        {
            battleHud.GetComponentInChildren<BattleHud>().Construct(player.GetComponent<PlayerHealth>(), player.GetComponent<PlayerSpeed>());
        }

        private void InformProgressReaders()
        {
            foreach (ILoadProgress progressReader in _gameFactory.ProgressReaders)
                progressReader.Load(_progressService.Progress);            
        }

        public void Enter()
        {
            
        }
    }
}
