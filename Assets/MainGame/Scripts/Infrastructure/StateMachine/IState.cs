namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public interface IState : IExitable
    {
        public void Enter();
    }
}