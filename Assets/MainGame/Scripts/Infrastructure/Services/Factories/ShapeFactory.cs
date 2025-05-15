using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly Dictionary<Shape, ISpawnerService<Shape>> _shapeSpawners;
        private readonly IShapeResourceLoader _shapeResourcesLoader;
        
        public ShapeFactory(IShapeResourceLoader shapeResourcesLoader)
        {
            _shapeSpawners = new Dictionary<Shape, ISpawnerService<Shape>>();
            _shapeResourcesLoader = shapeResourcesLoader;
        }

        public Shape Spawn(ShapeKey shapeKey)
        {
            Shape shape;
            _shapeResourcesLoader.TryGetResources(shapeKey, out ShapeResource shapeResource);
            
            if (_shapeSpawners.ContainsKey(shapeResource.Shape) == false)
            {
                _shapeSpawners[shapeResource.Shape] = new SpawnerService<Shape>(shapeResource.Shape);
                shape =  _shapeSpawners[shapeResource.Shape].Spawn();
            }
            else
            {
                shape  = _shapeSpawners[shapeResource.Shape].Spawn();
            }
            
            shape.Initialize(shapeResource.AnimalImage, shapeResource.ColorMaterial, shapeKey);
            
            return shape;
        }
    }
}