using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services
{
    public class CoroutineRunnerService : MonoBehaviour, ICoroutineRunnerService
    {
        public void StopCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            
            coroutine = null;
        }
    }
}