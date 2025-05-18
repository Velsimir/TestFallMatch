using System.Collections.Generic;

namespace MainGame.Scripts.Infrastructure.Services
{
    public class RestartRegistryService : IRestartRegistryService
    {
        private readonly List<IRestartable> _restartableObjects = new List<IRestartable>();

        public void Register(IRestartable restartable)
        {
            if (_restartableObjects.Contains(restartable))
            {
                return;
            }

            _restartableObjects.Add(restartable);
        }

        public void UnRegister(IRestartable restartable)
        {
            if (_restartableObjects.Contains(restartable) == false)
            {
                return;
            }

            _restartableObjects.Remove(restartable);
        }

        public void RestartAll()
        {
            foreach (var restartable in _restartableObjects)
            {
                restartable.Restart();
            }
        }
    }
}