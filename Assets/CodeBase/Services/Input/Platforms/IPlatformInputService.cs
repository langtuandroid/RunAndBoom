using System;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public interface IPlatformInputService : IService
    {
        public event Action<Vector2> Moved;
        public event Action Shot;
    }
}