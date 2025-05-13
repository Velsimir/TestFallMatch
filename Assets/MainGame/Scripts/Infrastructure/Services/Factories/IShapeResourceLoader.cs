using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public interface IShapeResourceLoader
    {
        public Dictionary<ShapeKey, ShapeResource> ShapeResources { get; }
        public bool TryGetResources(ShapeKey key, out ShapeResource resources);
        public ShapeKey GetRandomShapeKey();
    }
}