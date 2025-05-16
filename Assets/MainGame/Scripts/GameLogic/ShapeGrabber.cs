using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using UnityEngine;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeGrabber : MonoBehaviour
    {
        private List<Shape> _shapes;

        private void Awake()
        {
            _shapes = new List<Shape>();
        }

        private void OnEnable()
        {
            Shape.ShapeDisappeared += AddShape;
        }
        
        private void OnDisable()
        {
            Shape.ShapeDisappeared -= AddShape;
        }

        private void AddShape(Shape obj)
        {
            TryFindShape(obj);
            _shapes.Add(obj);
        }

        private void TryFindShape(Shape newShape)
        {
            foreach (var shape in _shapes)
            {
                if (newShape.GetHashCode() == shape.GetHashCode())
                {
                }
            }
        }
    }
}