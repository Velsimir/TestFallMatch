using System;

namespace MainGame.Scripts.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoaderService : IService
    {
        public void Load(SceneName sceneName, Action onLoaded = null);
    }
}