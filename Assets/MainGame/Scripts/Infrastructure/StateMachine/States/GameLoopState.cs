using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly Curtain _curtain;
        private readonly IRestartRegistryService _restartRegistryService;

        public GameLoopState(IGameStateMachine gameStateMachine, Curtain curtain, IRestartRegistryService  restartRegistryService)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
            _restartRegistryService = restartRegistryService;
        }

        public void Enter()
        {
            _curtain.Show();
            _curtain.Hide();
            
            _restartRegistryService.RestartAll();
        }

        public void Exit()
        {
        }
    }
}