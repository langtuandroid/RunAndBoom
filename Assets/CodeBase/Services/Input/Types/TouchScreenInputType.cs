using System;
using CodeBase.Data;
using CodeBase.Services.Input.Platforms;
using UnityEngine;
using UnityEngine.InputSystem;
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
            _playerInput.Player.SwipeContact.started -= StartTouchPrimary;
            _playerInput.Player.SwipeContact.canceled -= EndTouchPrimary;
            _playerInput.Player.DoubleTapContact.performed -= RotateToPoint;
        }

        private void InitTouchEvents()
        {
            _playerInput.Player.SwipeContact.started += StartTouchPrimary;
            _playerInput.Player.SwipeContact.canceled += EndTouchPrimary;
            _playerInput.Player.DoubleTapContact.performed += RotateToPoint;
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        public void OnDisable()
        {
            _playerInput.Disable();
        }

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (TouchedStart != null)
                TouchedStart(PrimaryPosition(_playerInput.Player.SwipePosition.ReadValue<Vector2>()),
                    (float)ctx.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            if (TouchedEnd != null)
                TouchedEnd(PrimaryPosition(_playerInput.Player.SwipePosition.ReadValue<Vector2>()), (float)ctx.time);
        }

        private Vector3 PrimaryPosition(Vector2 target) =>
            new Vector3(target.x, target.y).FromScreenToWorld(Camera.main);

        private void RotateToPoint(InputAction.CallbackContext ctx)
        {
            Vector3 raw = _playerInput.Player.DoubleTapPosition.ReadValue<Vector2>();
            Vector3 rotatingToPointV3 = PrimaryPosition(raw);
            Vector2 rotatingToPoint = new Vector2(rotatingToPointV3.x, rotatingToPointV3.z);
            ManualAimed?.Invoke(rotatingToPoint);
        }

        public class Factory : PlaceholderFactory<IPlatformInputService, TouchScreenInputType>
        {
        }
    }
}