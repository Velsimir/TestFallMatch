using MainGame.Scripts.Infrastructure.Services.SceneLoader;
using MainGame.Scripts.Infrastructure.StateMachine.States.StateInterfaces;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoaderService _sceneLoaderService;

        public BootstrapState(IGameStateMachine stateMachine, ISceneLoaderService sceneLoader)
        {
            _gameStateMachine = stateMachine;
            _sceneLoaderService = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoaderService.Load(SceneName.Game, LoadGameLoopState);
        }

        public void Exit()
        {
        }

        private void LoadGameLoopState()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}