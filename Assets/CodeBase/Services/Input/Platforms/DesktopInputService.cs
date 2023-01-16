using System;
using CodeBase.Services.Input.Types;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public class DesktopPlatformInputService : PlatformInputService
    {
        private readonly KeyboardMouseInputType _keyboardMouseInputType;

        public override event Action<Vector2> Moved;
        public override event Action<Vector2> Shot;

        public DesktopPlatformInputService(KeyboardMouseInputType.Factory keyboardMouseInputType)
        {
            _keyboardMouseInputType = keyboardMouseInputType.Create(this);
        }

        public void Destroy() =>
            UnsubscribeEvents();

        protected override void SubscribeEvents()
        {
        }

        protected override void UnsubscribeEvents()
        {
        }

        protected override void MoveTo(Vector2 direction)
        {
        }

        protected override void ManualAimedTo(Vector2 direction)
        {
        }
    }
}