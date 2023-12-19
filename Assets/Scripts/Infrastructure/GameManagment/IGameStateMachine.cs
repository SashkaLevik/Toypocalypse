using Assets.Scripts.Data.StaticData;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using Assets.Scripts.Player;

namespace Assets.Scripts.Infrastructure.GameManagment
{
    public interface IGameStateMachine : IService
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload sceneName) where TState : class, IPayloadedState<TPayload>;
        public void Enter<TState, TPayload>(TPayload sceneName, ToyStaticData toyData) where TState : class, IPayloadedState1<TPayload, ToyStaticData>;
    }
}
