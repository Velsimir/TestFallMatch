namespace MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces
{
    public interface IState : IExitable
    {
        public void Enter();
    }
}