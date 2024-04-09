using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Player;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.Enemyes;
using Assets.Scripts.GameEnvironment.Battle;

namespace Assets.Scripts.States
{
    public class LevelState : IPayloadedState1<string, ToyStaticData>, IState
    {
        private const string PlayerSpawnTag = "PlayerSpawnTag";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IToyDataService _toyDataService;
        private ToyStaticData _toyData;

        public LevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
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
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<LoopState>();
        }

        private void InitGameWorld()
        {
            GameObject spawnPoint = GameObject.FindWithTag(PlayerSpawnTag);
            var playerSpawner = spawnPoint.GetComponent<PlayerSpawnPoint>();
            playerSpawner.SetPosition(_toyData);
            GameObject player = _gameFactory.CreateToy(_toyData, spawnPoint);
            GameObject battleHud = _gameFactory.CreateBattleHud();
            GameObject skillPanel = _gameFactory.CreateSkillPanel();
            GameObject battleSystem = _gameFactory.CreateBattleSystem();
            GameObject artifactWatcher = _gameFactory.CreateArtifactsWatcher();
            artifactWatcher.GetComponent<ArtifactsWatcher>().Construct(skillPanel.GetComponent<SkillPanel>(), battleHud.GetComponentInChildren<PlayerHud>(),
                battleSystem.GetComponent<BattleSystem>());
            player.GetComponent<Toy>().Construct(skillPanel.GetComponent<SkillPanel>(), playerSpawner.RoutMap,
                battleHud.GetComponentInChildren<PlayerHud>());
            playerSpawner.GetPlayer(player.GetComponent<Toy>());
            battleSystem.GetComponent<BattleSystem>().Construct(playerSpawner, playerSpawner.GetComponent<EnemySpawner>(), playerSpawner.RoutMap);
            battleSystem.GetComponent<Settings>().Construct(playerSpawner);
            battleHud.GetComponent<AttackPanel>().Construct(playerSpawner.GetComponent<EnemySpawner>(), skillPanel.GetComponent<SkillPanel>());
            battleHud.GetComponentInChildren<PlayerHud>().Construct(player.GetComponent<Toy>(), player.GetComponent<PlayerHealth>(), player.GetComponent<PlayerSpeed>());
            skillPanel.GetComponent<SkillPanel>().Construct(player.GetComponent<Toy>(), battleHud.GetComponentInChildren<PlayerHud>(),
                battleSystem.GetComponent<BattleSystem>(), playerSpawner);
            //InitBattleHud(player, battleHud, skillPanel, battleSystem);
        }        
        
        //private void InitBattleHud(GameObject player, GameObject battleHud, GameObject skillPanel, GameObject battleSystem)
        //{
        //    battleHud.GetComponentInChildren<PlayerHud>().Construct(player.GetComponent<Toy>(), player.GetComponent<PlayerHealth>(), player.GetComponent<PlayerSpeed>());
        //    skillPanel.GetComponent<SkillPanel>().Construct(player.GetComponent<Toy>(), battleHud.GetComponentInChildren<PlayerHud>(), 
        //        battleSystem.GetComponent<BattleSystem>());
        //    //player.GetComponent<Toy>().Construct(skillPanel.GetComponent<SkillPanel>());
        //    //attackPanel.Construct(skillPanel.GetComponent<SkillPanel>());
        //}

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
