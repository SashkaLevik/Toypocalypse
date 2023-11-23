using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.States;
using Assets.Scripts.Toys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.GameManagment
{
    public interface IGameStateMachine : IService
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        public void Enter<TState, TPayload>(TPayload payload, Toy toy) where TState : class, IPayloadedState1<TPayload, Toy>;
    }
}
