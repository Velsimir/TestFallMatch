using System.Collections;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services
{
    public interface ICoroutineRunnerService : IService
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(ref Coroutine coroutine);
    }
}