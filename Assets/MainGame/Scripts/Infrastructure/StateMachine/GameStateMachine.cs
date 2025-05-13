using System;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces;

namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitable> _states;
        private IExitable _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IExitable>();
        }

        public void AddState<TState>(TState state) where TState : class, IExitable
        {
            _states[typeof(TState)] = state;
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeActiveState<TState>();
            state?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeActiveState<TState>();
            state?.Enter(payload);
        }

        private TState ChangeActiveState<TState>() where TState : class, IExitable
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitable
        {
            return _states[typeof(TState)] as TState;
        }
    }
}