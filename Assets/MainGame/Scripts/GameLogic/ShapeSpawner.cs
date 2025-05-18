using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Extensions;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using MainGame.Scripts.Infrastructure.Services.Factories;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;

namespace MainGame.Scripts.GameLogic
{
    public class ShapeSpawner
    {
        private readonly ICoroutineRunnerService _coroutineRunner;
        private readonly IShapeFactory _shapeFactory;
        private WaitForSeconds _waitBetweenSpawn;
        private Coroutine _coroutineSpawn;
        
        public event Action<ISpawnable> Spawned;

        private ShapeSpawner(IShapeFactory shapeFactory, ICoroutineRunnerService coroutineRunner)
        {
            _shapeFactory = shapeFactory;
            _coroutineRunner =  coroutineRunner;
        }

        public void StartSpawn(List<ShapeKey> shapeKeys, Transform spawnPoint, float timeBetweenSpawn)
        {
            _waitBetweenSpawn = new WaitForSeconds(timeBetweenSpawn);

            if (_coroutineSpawn != null)
            {
                _coroutineRunner.StopCoroutine(ref _coroutineSpawn);
                _coroutineSpawn = null;
            }

            _coroutineSpawn = _coroutineRunner.StartCoroutine(Spawn(shapeKeys, spawnPoint));
        }

        private IEnumerator Spawn(List<ShapeKey> shapeKeys, Transform spawnPoint)
        {
            shapeKeys.Shuffle();
            
            foreach (var shapeKey in shapeKeys)
            {
                ISpawnable shape = _shapeFactory.Spawn(shapeKey, spawnPoint);
                
                Spawned?.Invoke(shape);
                
                yield return _waitBetweenSpawn;
            }
        }
    }
}