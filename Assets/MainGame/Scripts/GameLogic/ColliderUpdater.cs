using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Scripts.GameLogic
{
    [RequireComponent(typeof(SpriteRenderer), 
        typeof(PolygonCollider2D))]
    public class ColliderUpdater : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PolygonCollider2D _polygonCollider2D;

        public void UpdateCollider()
        {
            var sprite = _spriteRenderer.sprite;
            _polygonCollider2D.pathCount = sprite.GetPhysicsShapeCount();

            var path = new List<Vector2>();
            for (int i = 0; i < _polygonCollider2D.pathCount; i++)
            {
                sprite.GetPhysicsShape(i, path);
                _polygonCollider2D.SetPath(i, path.ToArray());
            }
        }
    }
}