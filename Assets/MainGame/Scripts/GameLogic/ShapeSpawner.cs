using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using Zenject;
using MainGame.Scripts.Infrastructure.Services.Factories;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeSpawner : MonoBehaviour
    {
        private IShapeFactory _shapeFactory;
        private IShapeResourceLoader _shapeResourceLoader;
        private IInputClickHandlerService _inputClickHandlerService;
        
        private void OnEnable()
        {
            _inputClickHandlerService.ClickPressed += Spawn;
        }
        
        private void OnDisable()
        {
            _inputClickHandlerService.ClickPressed -= Spawn;
        }

        private void Spawn(Vector2 obj)
        {
            _shapeFactory.Spawn(_shapeResourceLoader.GetRandomShapeKey());
        }

        [Inject]
        private void Construct(IShapeFactory shapeFactory, IShapeResourceLoader shapeResourceLoader, IInputClickHandlerService inputClickHandlerService)
        {
            _shapeFactory = shapeFactory;
            _shapeResourceLoader = shapeResourceLoader;
            _inputClickHandlerService = inputClickHandlerService;
        }
    }
}