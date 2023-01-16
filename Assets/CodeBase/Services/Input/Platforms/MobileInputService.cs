using System;
using CodeBase.Services.Input.Types;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public class MobilePlatformInputService : PlatformInputService
    {
        private readonly TouchScreenInputType _touchScreenInputType;
        private readonly SwipeDetection _swipeDetection;

        public override event Action<Vector2> Moved;
        public override event Action<Vector2> Shot;

        public MobilePlatformInputService(TouchScreenInputType.Factory touchScreenInputType, SwipeDetection.Factory swipeDetection)
        {
            _touchScreenInputType = touchScreenInputType.Create(this);
            _swipeDetection = swipeDetection.Create(this);

            SubscribeEvents();
        }

        protected override void SubscribeEvents()
        {
            _swipeDetection.Swipe += MoveTo;
            _touchScreenInputType.ManualAimed += ManualAimedTo;
        }

        protected override void UnsubscribeEvents()
        {
            _swipeDetection.Swipe -= MoveTo;
            _touchScreenInputType.ManualAimed -= ManualAimedTo;
        }

        protected override void MoveTo(Vector2 direction) =>
            Moved?.Invoke(direction);

        protected override void ManualAimedTo(Vector2 direction) =>
            Shot?.Invoke(direction);
    }
}