namespace MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces
{
    public interface IPayloadState<TPayload> : IExitable
    {
        public void Enter(TPayload payload);
    }
}