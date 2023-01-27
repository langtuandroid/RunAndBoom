using System;
using CodeBase.Services.Input.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input.Types
{
    public class TouchScreenInputType : IInputTypeService
    {
        private readonly PlayerInput _playerInput;
        private Vector2 _aimPoint;
        private Vector2 _direction;

        #region Events

        public delegate void StartTouch(Vector2 position, float time);

        public event StartTouch TouchedStart;

        public delegate void EndTouch(Vector2 position, float time);

        public event EndTouch TouchedEnd;

        public event Action<Vector2> ManualAimed;

        #endregion

        public TouchScreenInputType(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            OnEnable();

            InitTouchEvents();
        }

        public void Destroy() =>
            UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
        }

        private void InitTouchEvents()
        {
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        public void OnDisable()
        {
            _playerInput.Disable();
        }

        public class Factory : PlaceholderFactory<IPlatformInputService, TouchScreenInputType>
        {
        }
    }
}