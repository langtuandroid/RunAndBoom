using System;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public abstract class PlatformInputService : IPlatformInputService
    {
        public abstract event Action<Vector2> Moved;
        public abstract event Action Shot;
        public abstract event Action ChoseWeapon1;
        public abstract event Action ChoseWeapon2;
        public abstract event Action ChoseWeapon3;
        public abstract event Action ChoseWeapon4;

        protected abstract void SubscribeEvents();

        protected abstract void UnsubscribeEvents();

        protected abstract void MoveTo(Vector2 direction);

        protected abstract void ShootTo();
    }
}