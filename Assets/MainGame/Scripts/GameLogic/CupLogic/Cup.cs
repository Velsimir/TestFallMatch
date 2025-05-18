using System;
using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeBorderLogic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.Factories;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainGame.Scripts.GameLogic.CupLogic
{
    public class Cup : MonoBehaviour, IRestartable
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _countOfVariableShapes;
        [SerializeField] private int _countOfCopiesShapes = 3;
        [SerializeField] private float _timeBetweenSpawn;
        [SerializeField] private Button _refreshCup;

        private List<ISpawnable> _currentShapes;
        private List<ShapeKey> _shapeKeys;
        private ShapeSpawner _shapeSpawner;
        private IShapeResourceLoader _shapeResourceLoader;
        private IRestartRegistryService _restartRegistryService;

        public static event Action CupEmptied;
        
        [Inject]
        private void Construct(IShapeResourceLoader shapeResourceLoader, IRestartRegistryService restartRegistryService, ShapeSpawner shapeSpawner)
        {
            _currentShapes = new List<ISpawnable>();
            _shapeKeys = new List<ShapeKey>();
            _restartRegistryService = restartRegistryService;
            _shapeResourceLoader =  shapeResourceLoader;
            _shapeSpawner =  shapeSpawner;
        }

        private void OnEnable()
        {
            MatchShapeCleaner.ShapeRemoved += RemoveShape;
            _refreshCup.onClick.AddListener(RespawnShapes);
            _shapeSpawner.Spawned += AddSpawnedShape;
            _restartRegistryService.Register(this);
        }

        private void OnDisable()
        {
            MatchShapeCleaner.ShapeRemoved -= RemoveShape;
            _refreshCup.onClick.RemoveListener(RespawnShapes);
            _shapeSpawner.Spawned -= AddSpawnedShape;
            _restartRegistryService.UnRegister(this);
        }

        public void Restart()
        {
            ClearCup();

            FillShapeKeys();

            RespawnShapes();
        }

        private void ClearCup()
        {
            foreach (var shape in _currentShapes)
            {
                shape.Disappear();
            }
            
            _currentShapes.Clear();
            _shapeKeys.Clear();
        }

        private void RespawnShapes()
        {
            foreach (var shape in _currentShapes)
            {
                shape.Disappear();
            }

            _currentShapes.Clear();

            _shapeSpawner.StartSpawn(new List<ShapeKey>(_shapeKeys), _spawnPoint, _timeBetweenSpawn);
        }

        private void AddSpawnedShape(ISpawnable shape)
        {
            _currentShapes.Add(shape);
        }

        private void RemoveShape(Shape shape)
        {
            _currentShapes.Remove(shape);
            _shapeKeys.Remove(shape.ShapeKey);

            if (_currentShapes.Count <= 0)
            {
                CupEmptied?.Invoke();
            }
        }

        private void FillShapeKeys()
        {
            for (int i = 0; i < _countOfVariableShapes; i++)
            {
                ShapeKey shapeKey = _shapeResourceLoader.GetRandomShapeKey();

                for (int j = 0; j < _countOfCopiesShapes; j++)
                {
                    _shapeKeys.Add(shapeKey);
                }
            }
        }
    }
}