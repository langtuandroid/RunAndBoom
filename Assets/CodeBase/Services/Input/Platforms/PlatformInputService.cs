using System;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public abstract class PlatformInputService : IPlatformInputService
    {
        public abstract event Action<Vector2> Moved;
        public abstract event Action Shot;

        protected abstract void SubscribeEvents();

        protected abstract void UnsubscribeEvents();

        protected abstract void MoveTo(Vector2 direction);

        protected abstract void ShotTo();
    }
}