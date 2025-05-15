using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainGame.Scripts.Infrastructure.Services
{
    public class InputClickHandler : IInputClickHandlerService, IDisposable
    {
        private readonly InputSystem _inputSystem;
        
        public InputClickHandler()
        {
            _inputSystem = new InputSystem();
            
            _inputSystem.Enable();
            
            _inputSystem.Player.Attack.performed += HandleClick;
        }
        
        public event Action<Vector2> ClickPressed;

        public void Dispose()
        {
            _inputSystem.Disable();
            _inputSystem.UI.Click.performed -= HandleClick;
        }

        private void HandleClick(InputAction.CallbackContext obj)
        {
            Vector2 newMousePosition = _inputSystem.UI.Point.ReadValue<Vector2>();
            ClickPressed?.Invoke(newMousePosition);
        }
    }
}