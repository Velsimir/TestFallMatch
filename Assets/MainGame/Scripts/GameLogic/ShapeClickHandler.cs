using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeClickHandler : MonoBehaviour
    {
        private IInputClickHandlerService _inputClickHandlerService;
        private Camera _camera;
        
        [Inject]
        private void Construct(IInputClickHandlerService inputClickHandlerService)
        {
            _inputClickHandlerService = inputClickHandlerService;
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputClickHandlerService.ClickPressed += HandleClick;
        }

        private void OnDisable()
        {
            _inputClickHandlerService.ClickPressed -= HandleClick;
        }

        private void HandleClick(Vector2 mousePosition)
        {
            Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Shape shape =  hit.collider.GetComponentInParent<Shape>();
                shape.Interact();
            }
        }
    }
}