using MainGame.Scripts.Infrastructure.StateMachine;
using MainGame.Scripts.Infrastructure.StateMachine.Services;

namespace MainGame.Scripts.Infrastructure
{
    public class Game
    {
        private readonly Curtain _curtainPrefab;
        public GameStateMachine GameStateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, Curtain curtainPrefab)
        {
            _curtainPrefab = curtainPrefab;
            GameStateMachine = new GameStateMachine(coroutineRunner);
        }
    }
}