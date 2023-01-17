using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input.Platforms
{
    public class EditorPlatformInputService : PlatformInputService
    {
        private readonly DesktopPlatformInputService _desktopPlatformInputService;
        private readonly MobilePlatformInputService _mobilePlatformInputService;

        public override event Action<Vector2> Moved;
        public override event Action<Vector2> Shot;

        [Inject]
        public EditorPlatformInputService(DesktopPlatformInputService desktopPlatformInputService,
            MobilePlatformInputService mobilePlatformInputService)
        {
            _desktopPlatformInputService = desktopPlatformInputService;
            _mobilePlatformInputService = mobilePlatformInputService;

            SubscribeEvents();
        }

        public void Destroy() =>
            UnsubscribeEvents();

        protected override void SubscribeEvents()
        {
            _desktopPlatformInputService.Moved += MoveTo;
            _mobilePlatformInputService.Moved += MoveTo;

            _desktopPlatformInputService.Shot += ManualAimedTo;
            _mobilePlatformInputService.Shot += ManualAimedTo;
        }

        protected override void UnsubscribeEvents()
        {
            _desktopPlatformInputService.Moved -= MoveTo;
            _mobilePlatformInputService.Moved -= MoveTo;

            _desktopPlatformInputService.Shot -= ManualAimedTo;
            _mobilePlatformInputService.Shot -= ManualAimedTo;
        }

        protected override void MoveTo(Vector2 direction) =>
            Moved?.Invoke(direction);

        protected override void ManualAimedTo(Vector2 direction) =>
            Shot?.Invoke(direction);
    }
}