namespace MainGame.Scripts.Infrastructure.Services
{
    public interface IRestartRegistryService : IService
    {
        public void Register(IRestartable restartable);
        public void UnRegister(IRestartable restartable);
        public void RestartAll();
    }
}