using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload sceneName);
    }

    public interface IPayloadedState1<TPayload, TPayload1> : IExitableState
    {
        void Enter(TPayload sceneName, TPayload1 toy);
    }

    public interface IExitableState
    {
        void Exit();
    }
}
