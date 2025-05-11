using MainGame.Scripts.Infrastructure.StateMachine;
using MainGame.Scripts.Infrastructure.StateMachine.Services;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain _curtainPrefab;
        
        private Game _game;
        private void Awake()
        {
            _curtainPrefab = Instantiate(_curtainPrefab);
            
            _game = new Game(coroutineRunner: this, _curtainPrefab);
            _game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        public void StopCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            
            coroutine = null;
        }
    }
}