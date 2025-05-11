using MainGame.Scripts.Infrastructure.StateMachine.Services;

namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoaderService _sceneLoaderService;

        public BootstrapState(GameStateMachine gameStateMachine, ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoaderService = new SceneLoaderService(coroutineRunner);
        }

        public void Enter()
        {
            _sceneLoaderService.Load(SceneName.Game);
        }

        public void Exit()
        {
        }
    }
}