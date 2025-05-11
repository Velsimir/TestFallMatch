using System;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.StateMachine.Services;

namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitable> _states;
        private IExitable _currentState;

        public GameStateMachine(ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitable>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, coroutineRunner),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
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