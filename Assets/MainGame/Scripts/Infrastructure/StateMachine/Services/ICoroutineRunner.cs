using System.Collections;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.StateMachine.Services
{
    public interface ICoroutineRunner : IService
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(ref Coroutine coroutine);
    }
}