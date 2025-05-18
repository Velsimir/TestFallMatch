using System;

namespace MainGame.Scripts.GameLogic.ShapeLogic
{
    public struct ShapeKey : IEquatable<ShapeKey>
    {
        private string ShapeVariable { get; }
        private string Color { get; }
        private string Animal { get; }

        public ShapeKey(string shapeVariable, string color, string animal)
        {
            ShapeVariable = shapeVariable;
            Color = color;
            Animal = animal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ShapeVariable, Color, Animal);
        }

        public bool Equals(ShapeKey other)
        {
            return ShapeVariable == other.ShapeVariable
                   && Color == other.Color
                   && Animal == other.Animal;
        }

        public override bool Equals(object obj)
        {
            return obj is ShapeKey other && Equals(other);
        }

        public override string ToString()
        {
            return $"{ShapeVariable}_{Color}_{Animal}";
        }
    }
}