using System;
using CodeBase.Services.Input.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input.Types
{
    public class SwipeDetection : IInputTypeService
    {
        private float _minDistance = 0.05f;
        private float _maxTime = 1f;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private float _startTime;
        private float _endTime;

        private readonly TouchScreenInputType _touchScreenInputType;

        public event Action<Vector2> Swipe;

        public SwipeDetection(TouchScreenInputType touchScreenInputType)
        {
            _touchScreenInputType = touchScreenInputType;

            SubscribeEvents();
        }

        public void Destroy() =>
            UnsubscribeEvents();

        private void SubscribeEvents()
        {
            _touchScreenInputType.TouchedStart += SwipeTouchedStart;
            _touchScreenInputType.TouchedEnd += SwipeTouchedEnd;
        }

        private void UnsubscribeEvents()
        {
            _touchScreenInputType.TouchedStart -= SwipeTouchedStart;
            _touchScreenInputType.TouchedEnd -= SwipeTouchedEnd;
        }

        private void SwipeTouchedStart(Vector2 position, float time)
        {
            _startPosition = position;
            _startTime = time;
        }

        private void SwipeTouchedEnd(Vector2 position, float time)
        {
            _endPosition = position;
            _endTime = time;
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            float xDelta = _endPosition.x - _startPosition.x;
            float time = _endTime - _startTime;

            if (Mathf.Abs(xDelta) >= _minDistance && time <= _maxTime)
            {
                Vector2 direction = xDelta > 0 ? Vector2.up : Vector2.down;
                Swipe?.Invoke(direction);
            }
        }

        public class Factory : PlaceholderFactory<IPlatformInputService, SwipeDetection>
        {
        }
    }
}