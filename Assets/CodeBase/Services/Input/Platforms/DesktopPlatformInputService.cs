using System;
using CodeBase.Services.Input.Types;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public class DesktopPlatformInputService : PlatformInputService
    {
        private readonly KeyboardMouseInputType _keyboardMouseInputType;

        public override event Action<Vector2> Moved;
        public override event Action Shot;

        public DesktopPlatformInputService(KeyboardMouseInputType.Factory keyboardMouseInputType)
        {
            _keyboardMouseInputType = keyboardMouseInputType.Create(this);

            SubscribeEvents();
        }

        public DesktopPlatformInputService(KeyboardMouseInputType keyboardMouseInputType)
        {
            _keyboardMouseInputType = keyboardMouseInputType;

            SubscribeEvents();
        }

        public void Destroy() =>
            UnsubscribeEvents();

        protected override void SubscribeEvents()
        {
            _keyboardMouseInputType.Moved += MoveTo;
            _keyboardMouseInputType.Shot += ShotTo;
        }

        protected override void UnsubscribeEvents()
        {
            _keyboardMouseInputType.Moved -= MoveTo;
            _keyboardMouseInputType.Shot -= ShotTo;
        }


        protected override void MoveTo(Vector2 direction) =>
            Moved?.Invoke(direction);

        protected override void ShotTo() =>
            Shot?.Invoke();
    }
}