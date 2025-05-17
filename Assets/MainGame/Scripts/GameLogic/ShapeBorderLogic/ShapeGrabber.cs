using System;
using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainGame.Scripts.GameLogic.ShapeBorderLogic
{
    public class ShapeGrabber : MonoBehaviour, IRestartable
    {
        [SerializeField] private List<ShapeSpawnPoint> _spawnPoints;
        [SerializeField] private int _similarShapesToStreak = 3;
        [SerializeField] private Button _restartButton;
        
        private Dictionary<Shape, ShapeSpawnPoint> _allShapes;
        private List<Shape> _similarShapes;
        private IRestartRegistryService _restartRegistryService;
        
        public static event Action FilledUp;
        public static event Action<Shape> ShapeRemoved;
        
        private int MaxCountOfShapes => _spawnPoints.Count;

        [Inject]
        private void Construct(IRestartRegistryService restartRegistryService)
        {
            _allShapes = new Dictionary<Shape, ShapeSpawnPoint>();
            _similarShapes = new List<Shape>();
            _restartRegistryService = restartRegistryService;
        }

        private void OnEnable()
        {
            Shape.SentToBar += GetShape;
            _restartRegistryService.Register(this);
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            Shape.SentToBar -= GetShape;
            _restartRegistryService.UnRegister(this);
            _restartButton.onClick.RemoveListener(Restart);
        }

        public void Restart()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.UnReserve();
            }
            
            _allShapes.Clear();
            _similarShapes.Clear();
        }

        private void GetShape(Shape shape)
        {
            AddShape(shape);
        }

        private bool CheckIsFull()
        {
            return _allShapes.Keys.Count >= MaxCountOfShapes;
        }

        private void AddShape(Shape shape)
        {
            foreach (ShapeSpawnPoint spawnPoint in _spawnPoints)
            {
                if (spawnPoint.IsReserved == true)
                {
                    continue;
                }
                
                _allShapes[shape] = spawnPoint;;
                shape.SetInBar(spawnPoint.Transform);
                spawnPoint.Reserve();

                break;
            }
            
            TryFindSimilarShapes(shape);
            
            if (CheckIsFull() == true)
            {
                FilledUp?.Invoke();
            }
        }

        private void TryFindSimilarShapes(Shape newShape)
        {
            foreach (var shape in _allShapes)
            {
                if (newShape.ShapeKey.GetHashCode() == shape.Key.ShapeKey.GetHashCode())
                {
                    _similarShapes.Add(shape.Key);
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
                _allShapes[shape].UnReserve();
                shape.Disappear();
                _allShapes.Remove(shape);
                ShapeRemoved?.Invoke(shape);
            }
        }
    }
}