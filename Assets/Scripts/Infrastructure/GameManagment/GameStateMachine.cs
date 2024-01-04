using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.GameManagment
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(MenuState)] = new MenuState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()),

                [typeof(LevelState)] = new LevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>(), services.Single<IToyDataService>()),

                [typeof(LoopState)] = new LoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload sceneName) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(sceneName);
        }

        public void Enter<TState, TPayload>(TPayload sceneName, ToyStaticData toyData) where TState : class, IPayloadedState1<TPayload, ToyStaticData>
        {
            TState state = ChangeState<TState>();
            state.Enter(sceneName, toyData);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}