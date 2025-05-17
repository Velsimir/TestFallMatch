using MainGame.Scripts.Infrastructure.StateMachine;
using MainGame.Scripts.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainGame.Scripts.GameLogic
{
    public class GameRestarter : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private IGameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }
        
        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}