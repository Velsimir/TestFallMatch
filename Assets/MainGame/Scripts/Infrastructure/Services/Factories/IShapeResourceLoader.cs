using MainGame.Scripts.GameLogic.ShapeLogic;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public interface IShapeResourceLoader
    {
        public bool TryGetResources(ShapeKey key, out ShapeResource resources);
        public ShapeKey GetRandomShapeKey();
    }
}