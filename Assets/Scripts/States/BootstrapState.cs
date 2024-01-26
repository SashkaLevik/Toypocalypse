using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.GameManagment;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Services;

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
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterToyData();
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>()));
        }

        private void EnterState()
        {
            //_stateMachine.Enter<MenuState, string>(Menu);
            _stateMachine.Enter<ProgressState>();
        }

        private void RegisterToyData()
        {
            IToyDataService toyData = new ToyDataService();
            toyData.Load();
            _services.RegisterSingle(toyData);
        }
    }
}
