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
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private Quaternion _defaultRotation;
        
        public event Action<ISpawnable> Disappeared;
        public static event Action<Shape> SentToBar;

        public ShapeKey ShapeKey { get; private set; }

        private void Awake()
        {
            _defaultRotation = transform.rotation;
        }

        public void Initialize(Sprite animalImage, Material colorMaterial, ShapeKey shapeKey)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            
            _animalSprite.sprite = animalImage;
            _colorMaterial.color = colorMaterial.color;
            _fillSprite.color = colorMaterial.color;

            transform.rotation =  _defaultRotation;
            
            ShapeKey = shapeKey;
        }

        public void Interact()
        {
            SentToBar?.Invoke(this);
        }

        public void SetInBar(Transform position)
        {
            transform.position = position.position;
            transform.rotation = _defaultRotation;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}