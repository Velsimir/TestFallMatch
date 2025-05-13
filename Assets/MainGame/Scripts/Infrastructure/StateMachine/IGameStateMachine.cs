using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces;

namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public interface IGameStateMachine : IService
    {
        public void AddState<TState>(TState state) where TState : class, IExitable;
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }
}