using UnityEngine;
using Zenject;
using MainGame.Scripts.Infrastructure.Services.Factories;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeSpawner : MonoBehaviour
    {
        private IShapeFactory _shapeFactory;
        private IShapeResourceLoader _shapeResourceLoader;

        private void Awake()
        {
           _shapeFactory.Spawn(_shapeResourceLoader.GetRandomShapeKey());
        }

        [Inject]
        private void Construct(IShapeFactory shapeFactory, IShapeResourceLoader shapeResourceLoader)
        {
            _shapeFactory = shapeFactory;
            _shapeResourceLoader = shapeResourceLoader;
        }
    }
}