using MainGame.Scripts.GameLogic.ShapeLogic;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public interface IShapeFactory : IService
    {
        public Shape Spawn(ShapeKey shapeKey, Transform position = null);
    }
}