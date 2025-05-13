using MainGame.Scripts.GameLogic.ShapeLogic;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public interface IShapeFactory : IService
    {
        public Shape Spawn(ShapeKey shapeKey);
    }
}