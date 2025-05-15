using System;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services
{
    public interface IInputClickHandlerService : IService
    {
        event Action<Vector2> ClickPressed;
    }
}