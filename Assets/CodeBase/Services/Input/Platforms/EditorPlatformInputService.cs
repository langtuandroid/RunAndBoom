using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input.Platforms
{
    public class EditorPlatformInputService : PlatformInputService
    {
        private readonly IPlatformInputService _platformInputService1;
        private readonly IPlatformInputService _platformInputService2;

        public override event Action<Vector2> Moved;
        public override event Action<Vector2> Shot;

        [Inject]
        public EditorPlatformInputService(IPlatformInputService platformInputService1, IPlatformInputService platformInputService2)
        {
            _platformInputService1 = platformInputService1;
            _platformInputService2 = platformInputService2;

            SubscribeEvents();
        }

        public void Destroy() =>
            UnsubscribeEvents();

        protected override void SubscribeEvents()
        {
            _platformInputService1.Moved += MoveTo;
            _platformInputService2.Moved += MoveTo;

            _platformInputService1.Shot += ShotTo;
            _platformInputService2.Shot += ShotTo;
        }

        protected override void UnsubscribeEvents()
        {
            _platformInputService1.Moved -= MoveTo;
            _platformInputService2.Moved -= MoveTo;

            _platformInputService1.Shot -= ShotTo;
            _platformInputService2.Shot -= ShotTo;
        }

        protected override void MoveTo(Vector2 direction) =>
            Moved?.Invoke(direction);

        protected override void ShotTo(Vector2 direction) =>
            Shot?.Invoke(direction);
    }
}