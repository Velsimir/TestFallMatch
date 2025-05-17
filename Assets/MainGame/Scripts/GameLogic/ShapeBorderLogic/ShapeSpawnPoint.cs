using UnityEngine;

namespace MainGame.Scripts.GameLogic.ShapeBorderLogic
{
    public class ShapeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        
        public bool IsReserved { get; private set; } = false;
        public Transform Transform => _transform;
        
        public void Reserve()
        {
            IsReserved = true;
        }

        public void UnReserve()
        {
            IsReserved = false;
        }
    }
}