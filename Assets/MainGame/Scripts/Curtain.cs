using System.Collections;
using UnityEngine;

namespace MainGame.Scripts
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private float _alphaStep = 0.1f;
        [SerializeField] private float _timeFadeStep = 0.01f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private WaitForSeconds _waitBetweenSteps;
        
        private void Awake()
        {
            _waitBetweenSteps = new WaitForSeconds(_timeFadeStep);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }
        
        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _alphaStep;
                
                yield return _waitBetweenSteps;
            }
            
            gameObject.SetActive(false);
        }
    }
}