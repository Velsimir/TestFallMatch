using UnityEngine;

namespace MainGame.Scripts.GameLogic.ShapeLogic
{
    [CreateAssetMenu(fileName = "Shapes", menuName = "ShapeVariable", order = 0)]
    public class ShapeVariable : ScriptableObject
    {
        [SerializeField] private Sprite _borderImage;
        [SerializeField] private Sprite _fillImage;
        [SerializeField] private Shape _shape;

        public Sprite BorderImage => _borderImage;
        public Sprite FillImage => _fillImage;
        public Shape Shape => _shape;
    }
}