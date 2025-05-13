using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGame.Scripts.Infrastructure.Services.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private ICoroutineRunnerService _coroutineRunnerService;

        public SceneLoaderService(ICoroutineRunnerService coroutineRunnerService)
        {
            _coroutineRunnerService = coroutineRunnerService;
        }

        public SceneName CurrentSceneName { get; private set; }

        public void Load(SceneName sceneName, Action onLoaded = null)
        {
            CurrentSceneName = sceneName;
            _coroutineRunnerService.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        private IEnumerator LoadScene(SceneName sceneName, Action onLoaded = null)
        {
            string name = sceneName.ToString();
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);

            while (asyncOperation.isDone == true)
            {
                yield return null;
            }
            
            onLoaded?.Invoke();
        }
    }
}