using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Extensions;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using Zenject;
using MainGame.Scripts.Infrastructure.Services.Factories;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private int _countOfVariableShapes;
        [SerializeField] private int _countOfCopiesShapes = 3;
        [SerializeField] private float _timeBetweenSpawn;

        private IShapeFactory _shapeFactory;
        private IShapeResourceLoader _shapeResourceLoader;
        private List<ShapeKey> _shapeKeys;
        private List<Shape> _currentShapes;
        private WaitForSeconds _waitBetweenSpawn;
        private Coroutine _coroutineSpawn;
        
        public List<Shape> CurrentShapes => new List<Shape>(_currentShapes);

        [Inject]
        private void Construct(IShapeFactory shapeFactory, IShapeResourceLoader shapeResourceLoader, IInputClickHandlerService inputClickHandlerService)
        {
            _shapeFactory = shapeFactory;
            _shapeResourceLoader = shapeResourceLoader;
        }

        private void Awake()
        {
            _transform = transform;
            _shapeKeys = new List<ShapeKey>();
            _currentShapes = new List<Shape>();
            _waitBetweenSpawn = new WaitForSeconds(_timeBetweenSpawn);

            FillShapeKeys();
            
            StartSpawn();
        }

        [ContextMenu("Respawn Shapes")]
        private void RespawnShapes()
        {
            foreach (var shape in _currentShapes)
            {
                shape.Disappear();
            }
            
            StartSpawn();
        }

        private void StartSpawn()
        {
            if (_coroutineSpawn != null)
            {
                StopCoroutine(_coroutineSpawn);
                _coroutineSpawn = null;
            }
            
            _coroutineSpawn = StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            _shapeKeys.Shuffle();

            foreach (var shapeKey in _shapeKeys)
            {
                _currentShapes.Add(_shapeFactory.Spawn(shapeKey, _transform));
                
                yield return _waitBetweenSpawn;
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