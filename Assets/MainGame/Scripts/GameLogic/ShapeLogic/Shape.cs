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
        [SerializeField] private PolygonCollider2D _polygonCollider;
        
        public event Action<ISpawnable> Disappeared;

        public ShapeKey ShapeKey;

        public void Initialize(Sprite shapeBorder, Sprite fillSprite, Sprite animalImage, Material colorMaterial, ShapeKey shapeKey)
        {
            _shapeBorder.sprite = shapeBorder;
            _fillSprite.sprite = fillSprite;
            _animalSprite.sprite = animalImage;
            _colorMaterial.color = colorMaterial.color;
            _fillSprite.color = colorMaterial.color;

            ShapeKey = shapeKey;

            UpdateCollider();
        }

        public void Disappear()
        {
            Disappeared?.Invoke(this);
        }

        private void UpdateCollider()
        {
            var sprite = _shapeBorder.sprite;
            _polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

            var path = new List<Vector2>();
            for (int i = 0; i < _polygonCollider.pathCount; i++)
            {
                sprite.GetPhysicsShape(i, path);
                _polygonCollider.SetPath(i, path.ToArray());
            }
        }
    }
}