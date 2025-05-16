using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.GameLogic.ShapeLogic
{
    [RequireComponent(typeof(SpriteRenderer), 
        typeof(Rigidbody2D))]
    public class Shape : MonoBehaviour, ISpawnable
    {
        [SerializeField] private SpriteRenderer _shapeBorder;
        [SerializeField] private SpriteRenderer _fillSprite;
        [SerializeField] private SpriteRenderer _animalSprite;
        [SerializeField] private SpriteRenderer _colorMaterial;
        public event Action<ISpawnable> Disappeared;
        public static event Action<Shape> ShapeDisappeared;

        public ShapeKey ShapeKey { get; private set; }

        public void Initialize(Sprite animalImage, Material colorMaterial, ShapeKey shapeKey)
        {
            transform.Rotate(Vector3.zero);
            _animalSprite.sprite = animalImage;
            _colorMaterial.color = colorMaterial.color;
            _fillSprite.color = colorMaterial.color;

            ShapeKey = shapeKey;
        }

        public void Disappear()
        {
            ShapeDisappeared?.Invoke(this);
            gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}