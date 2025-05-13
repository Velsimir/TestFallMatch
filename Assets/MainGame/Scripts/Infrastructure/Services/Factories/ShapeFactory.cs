using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly ISpawnerService<Shape> _shapeSpawner;
        private readonly IShapeResourceLoader _shapeResourcesLoader;
        
        public ShapeFactory(ISpawnerService<Shape> shapeSpawner, IShapeResourceLoader shapeResourcesLoader)
        {
            _shapeSpawner = shapeSpawner;
            _shapeResourcesLoader = shapeResourcesLoader;
        }

        public Shape Spawn(ShapeKey shapeKey)
        {
            Shape shape = _shapeSpawner.Spawn();

            if (_shapeResourcesLoader.TryGetResources(shapeKey, out ShapeResource shapeResource))
            {
                shape.Initialize(shapeResource.BorderImage, shapeResource.FillImage, shapeResource.AnimalImage, shapeResource.ColorMaterial, shapeKey);
            }
            
            return shape;
        }
    }
}