using MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly Curtain _curtain;

        public GameLoopState(IGameStateMachine gameStateMachine, Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
        }

        public void Enter()
        {
            _curtain.Hide();
        }

        public void Exit()
        {
        }
    }
}