using UnityEngine;

namespace MainGame.Scripts.GameLogic.ShapeLogic
{
    public struct ShapeResource
    {
        public Sprite BorderImage { get; }
        public Sprite FillImage { get; }
        public Material ColorMaterial { get; }
        public Sprite AnimalImage { get; }

        public ShapeResource(Sprite borderImage, Sprite fillImage, Material colorMaterial, Sprite animalImage)
        {
            BorderImage = borderImage;
            FillImage = fillImage;
            ColorMaterial = colorMaterial;
            AnimalImage = animalImage;
        }
    }
}