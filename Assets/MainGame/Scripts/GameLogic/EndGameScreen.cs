using MainGame.Scripts.GameLogic.ShapeBorderLogic;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace MainGame.Scripts.GameLogic
{
    public class EndGameScreen : MonoBehaviour, IRestartable
    {
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _background;
        [SerializeField] private GameRestarter _gameRestarter;
        
        private IRestartRegistryService _restartRegistryService;

        [Inject]
        private void Construct(IRestartRegistryService restartRegistryService)
        {
            _restartRegistryService = restartRegistryService;
        }

        private void OnEnable()
        {
            ShapeGrabber.FilledUp += ShowLooseScreen;
            ShapeSpawner.CupEmptied += ShowWinScreen;
            _restartRegistryService.Register(this);
        }
        
        private void OnDisable()
        {
            ShapeGrabber.FilledUp -= ShowLooseScreen;
            ShapeSpawner.CupEmptied -= ShowWinScreen;
            _restartRegistryService.UnRegister(this);
        }

        public void Restart()
        {
            _winScreen.SetActive(false);
            _loseScreen.SetActive(false);
            _background.SetActive(false);
            _gameRestarter.gameObject.SetActive(false);
        }

        private void ShowWinScreen()
        {
            _winScreen.SetActive(true);
            _background.SetActive(true);
            _gameRestarter.gameObject.SetActive(true);
        }

        private void ShowLooseScreen()
        {
            _loseScreen.SetActive(true);
            _background.SetActive(true);
            _gameRestarter.gameObject.SetActive(true);
        }
    }
}