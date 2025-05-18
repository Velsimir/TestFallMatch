using System.Collections.Generic;
using System.Linq;
using MainGame.Scripts.GameLogic.ShapeLogic;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.Factories
{
    public class ShapeResourcesLoader : IShapeResourceLoader
    {
        private readonly Dictionary<ShapeKey, ShapeResource> _shapeResources;

        public ShapeResourcesLoader()
        {
            _shapeResources = new Dictionary<ShapeKey, ShapeResource>();

            LoadResources();
        }

        public bool TryGetResources(ShapeKey key, out ShapeResource resources) =>
            _shapeResources.TryGetValue(key, out resources);

        public ShapeKey GetRandomShapeKey()
        {
            List<ShapeKey> keys = _shapeResources.Keys.ToList();
            int randomIndex = Random.Range(0, keys.Count);
            return keys[randomIndex];
        }

        private void LoadResources()
        {
            ShapeVariable[] shapeVariables = Resources.LoadAll<ShapeVariable>("ShapesVariable");
            Material[] colors = Resources.LoadAll<Material>("Colors");
            Sprite[] animals = Resources.LoadAll<Sprite>("ImageAnimals");

            foreach (var shapeVariable in shapeVariables)
            {
                string shapeVariableName = shapeVariable.name;

                foreach (var color in colors)
                {
                    string colorName = color.name;

                    foreach (var animal in animals)
                    {
                        string animalName = animal.name;

                        ShapeKey key = new ShapeKey(shapeVariableName, colorName, animalName);

                        _shapeResources[key] = new ShapeResource(shapeVariable.Shape, shapeVariable.BorderImage,
                            shapeVariable.FillImage, color, animal);
                    }
                }
            }
        }
    }
}