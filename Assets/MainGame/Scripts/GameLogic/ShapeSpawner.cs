using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeBorderLogic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Extensions;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using Zenject;
using MainGame.Scripts.Infrastructure.Services.Factories;
using UnityEngine.UI;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeSpawner : MonoBehaviour, IRestartable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private int _countOfVariableShapes;
        [SerializeField] private int _countOfCopiesShapes = 3;
        [SerializeField] private float _timeBetweenSpawn;
        [SerializeField] private Button _reserveButton;
        
        private IShapeFactory _shapeFactory;
        private IShapeResourceLoader _shapeResourceLoader;
        private List<ShapeKey> _shapeKeys;
        private List<Shape> _currentShapes;
        private WaitForSeconds _waitBetweenSpawn;
        private Coroutine _coroutineSpawn;
        private IRestartRegistryService _restartRegistryService;

        public static event Action CupEmptied;
        
        [Inject]
        private void Construct(IShapeFactory shapeFactory, IShapeResourceLoader shapeResourceLoader, IInputClickHandlerService inputClickHandlerService, IRestartRegistryService restartRegistryService)
        {
            _shapeFactory = shapeFactory;
            _shapeResourceLoader = shapeResourceLoader;
            
            _transform = transform;
            _shapeKeys = new List<ShapeKey>();
            _currentShapes = new List<Shape>();
            _waitBetweenSpawn = new WaitForSeconds(_timeBetweenSpawn);
            _restartRegistryService =  restartRegistryService;
        }

        private void OnEnable()
        {
            ShapeGrabber.ShapeRemoved += RemoveShape;
            _reserveButton.onClick.AddListener(RespawnShapes);
            _restartRegistryService.Register(this);
            Debug.Log("_restartRegistryService.Register(this)");
        }
        
        private void OnDisable()
        {
            ShapeGrabber.ShapeRemoved -= RemoveShape;
            _reserveButton.onClick.RemoveListener(RespawnShapes);
            _restartRegistryService.UnRegister(this);
        }

        public void Restart()
        {
            foreach (var shape in _currentShapes)
            {
                shape.Disappear();
            }
            
            _shapeKeys.Clear();
            _currentShapes.Clear();
            
            FillShapeKeys();
            
            StartSpawn();
        }

        private void RespawnShapes()
        {
            //TODO доделать респавн фигурок, если заспавнились не все
            foreach (var shape in _currentShapes)
            {
                shape.Disappear();
            }
            
            _currentShapes.Clear();
            
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
                Shape shape = _shapeFactory.Spawn(shapeKey, _transform);
                
                _currentShapes.Add(shape);
                
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

        private void RemoveShape(Shape shape)
        {
            _currentShapes.Remove(shape);
            _shapeKeys.Remove(shape.ShapeKey);

            if (_currentShapes.Count <= 0)
            {
                CupEmptied?.Invoke();
            }
        }
    }
}