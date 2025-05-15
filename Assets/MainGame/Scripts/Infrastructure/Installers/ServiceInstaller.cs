using MainGame.Scripts.GameLogic.ShapeLogic;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.Factories;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Infrastructure.Services.SceneLoader;
using MainGame.Scripts.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace MainGame.Scripts.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] CoroutineRunnerService _coroutineRunnerServicePrefab;
        [SerializeField] Curtain _curtain;
        
        override public void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<IShapeResourceLoader>().To<ShapeResourcesLoader>().AsSingle();
            Container.Bind<IShapeFactory>().To<ShapeFactory>().AsSingle();
            Container.Bind<IInputClickHandlerService>().To<InputClickHandler>().AsSingle();
            
            Container.Bind<ICoroutineRunnerService>().FromComponentInNewPrefab(_coroutineRunnerServicePrefab).AsSingle();
            Container.Bind<Curtain>().FromComponentInNewPrefab(_curtain).AsSingle();
            
            Container.BindInterfacesAndSelfTo<Startup>().AsSingle().NonLazy();
        }
    }
}