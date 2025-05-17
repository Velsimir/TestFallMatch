using MainGame.Scripts.Infrastructure.StateMachine;
using MainGame.Scripts.Infrastructure.StateMachine.States;
using Zenject;

namespace MainGame.Scripts.Infrastructure.Installers
{
    public class Startup : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly BootstrapState _bootstrapState;
        private readonly GameLoopState _gameLoopState;

        public Startup(IGameStateMachine stateMachine, BootstrapState bootstrapState,GameLoopState gameLoopState)
        {
            _stateMachine = stateMachine;
            _bootstrapState = bootstrapState;
            _gameLoopState = gameLoopState;
        }
        
        public void Initialize()
        {
            _stateMachine.AddState(_bootstrapState);
            _stateMachine.AddState(_gameLoopState);

            _stateMachine.Enter<BootstrapState>();
        }
    }
}