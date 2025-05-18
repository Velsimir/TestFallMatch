using System;
using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainGame.Scripts.GameLogic.ShapeBorderLogic
{
    public class MatchShapeCleaner : MonoBehaviour, IRestartable
    {
        [SerializeField] private int _similarShapesToStreak = 3;
        [SerializeField] private Button _restartButton;
        [SerializeField] private ShapeHolderBar _shapeHolderBar;

        private List<Shape> _similarShapes;
        private List<Shape> _allShapes;
        private IRestartRegistryService _restartRegistryService;

        public static event Action FilledUp;
        public static event Action<Shape> ShapeRemoved;

        [Inject]
        private void Construct(IRestartRegistryService restartRegistryService)
        {
            _similarShapes = new List<Shape>();
            _allShapes = new List<Shape>();
            _restartRegistryService = restartRegistryService;
        }

        private void OnEnable()
        {
            Shape.SentToBar += AddShape;
            _restartRegistryService.Register(this);
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            Shape.SentToBar -= AddShape;
            _restartRegistryService.UnRegister(this);
            _restartButton.onClick.RemoveListener(Restart);
        }

        public void Restart()
        {
            _allShapes.Clear();
            _similarShapes.Clear();
        }

        private void AddShape(Shape shape)
        {
            _shapeHolderBar.SetShape(shape);
            _allShapes.Add(shape);
            TryFindSimilarShapes(shape);

            if (_shapeHolderBar.IsFull == true)
            {
                FilledUp?.Invoke();
            }
        }

        private void TryFindSimilarShapes(Shape newShape)
        {
            foreach (var shape in _allShapes)
            {
                if (newShape.ShapeKey.GetHashCode() == shape.ShapeKey.GetHashCode())
                {
                    _similarShapes.Add(shape);
                }

                if (_similarShapes.Count >= _similarShapesToStreak)
                {
                    DeactivateSimilarShapes();
                    break;
                }
            }

            _similarShapes.Clear();
        }

        private void DeactivateSimilarShapes()
        {
            foreach (var shape in _similarShapes)
            {
                _shapeHolderBar.UnReservePlace(shape);
                shape.Disappear();
                _allShapes.Remove(shape);
                ShapeRemoved?.Invoke(shape);
            }
        }
    }
}