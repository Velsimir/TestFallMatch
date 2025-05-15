using System;
using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.GameLogic.ShapeLogic
{
    [RequireComponent(typeof(SpriteRenderer), 
        typeof(Rigidbody2D), 
        typeof(PolygonCollider2D))]
    public class Shape : MonoBehaviour, ISpawnable
    {
        [SerializeField] private SpriteRenderer _shapeBorder;
        [SerializeField] private SpriteRenderer _fillSprite;
        [SerializeField] private SpriteRenderer _animalSprite;
        [SerializeField] private SpriteRenderer _colorMaterial;
        [SerializeField] private ColliderUpdater _borderColliderUpdater;
        [SerializeField] private ColliderUpdater _fillColliderUpdater;
        public event Action<ISpawnable> Disappeared;
        public static event Action<Shape> ShapeDisappeared;

        public ShapeKey ShapeKey { get; private set; }

        public void Initialize(Sprite shapeBorder, Sprite fillSprite, Sprite animalImage, Material colorMaterial, ShapeKey shapeKey)
        {
            _shapeBorder.sprite = shapeBorder;
            _fillSprite.sprite = fillSprite;
            _animalSprite.sprite = animalImage;
            _colorMaterial.color = colorMaterial.color;
            _fillSprite.color = colorMaterial.color;

            ShapeKey = shapeKey;
            
            _borderColliderUpdater.UpdateCollider();
            _fillColliderUpdater.UpdateCollider();
        }

        public void Disappear()
        {
            ShapeDisappeared?.Invoke(this);
            Debug.Log($"{ShapeKey.ShapeVariable} - {ShapeKey.Color} - {ShapeKey.Animal}");
            Disappeared?.Invoke(this);
        }
    }
}