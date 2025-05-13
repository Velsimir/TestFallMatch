using MainGame.Scripts.Infrastructure.StateMachine.States;
using Zenject;

namespace MainGame.Scripts.Infrastructure.Installers
{
    public class GameStateInstaller : MonoInstaller
    {
        override public void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
    }
}