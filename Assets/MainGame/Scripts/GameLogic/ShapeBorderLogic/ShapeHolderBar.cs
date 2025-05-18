using System.Collections.Generic;
using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainGame.Scripts.GameLogic.ShapeBorderLogic
{
    public class ShapeHolderBar : MonoBehaviour, IRestartable
    {
        [SerializeField] private List<ShapeSpawnPoint> _spawnPoints;
        [SerializeField] private Button _restartButton;
        
        private Dictionary<Shape, ShapeSpawnPoint> _allShapes;
        private IRestartRegistryService _restartRegistryService;

        public bool IsFull => _allShapes.Keys.Count >= _spawnPoints.Count;

        [Inject]
        private void Construct(IRestartRegistryService restartRegistryService)
        {
            _allShapes = new Dictionary<Shape, ShapeSpawnPoint>();
            _restartRegistryService = restartRegistryService;
        }

        private void OnEnable()
        {
            _restartRegistryService.Register(this);
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _restartRegistryService.UnRegister(this);
            _restartButton.onClick.RemoveListener(Restart);
        }

        public void SetShape(Shape shape)
        {
            foreach (ShapeSpawnPoint spawnPoint in _spawnPoints)
            {
                if (spawnPoint.IsReserved == true)
                {
                    continue;
                }
                
                _allShapes[shape] =  spawnPoint;
                shape.SetInBar(spawnPoint.Transform);
                spawnPoint.Reserve();

                break;
            }
        }

        public void Restart()
        {
            _allShapes.Clear();
            UnReserveAllPoints();
        }

        public void UnReservePlace(Shape shape)
        {
            _allShapes[shape].UnReserve();
            _allShapes.Remove(shape);
        }

        private void UnReserveAllPoints()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.UnReserve();
            }
        }
    }
}