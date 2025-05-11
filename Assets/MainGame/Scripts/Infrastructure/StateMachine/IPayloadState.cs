namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public interface IPayloadState<TPayload> : IExitable
    {
        public void Enter(TPayload payload);
    }
}