using UnityEngine;

namespace MainGame.Scripts.GameLogic.ShapeLogic
{
    public struct ShapeResource
    {
        public Shape Shape { get; }
        public Sprite BorderImage { get; }
        public Sprite FillImage { get; }
        public Material ColorMaterial { get; }
        public Sprite AnimalImage { get; }

        public ShapeResource(Shape shape, Sprite borderImage, Sprite fillImage, Material colorMaterial, Sprite animalImage)
        {
            Shape = shape;
            BorderImage = borderImage;
            FillImage = fillImage;
            ColorMaterial = colorMaterial;
            AnimalImage = animalImage;
        }
    }
}