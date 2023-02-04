using System;
using CodeBase.Services.Input.Platforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Services.Input.Types
{
    public class KeyboardMouseInputType : IInputTypeService
    {
        private readonly PlayerInput _playerInput;

        public event Action<Vector2> Moved;
        public event Action Shot;

        public KeyboardMouseInputType(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            SubscribeEvents();
        }

        public void Destroy() =>
            UnsubscribeEvents();

        private void SubscribeEvents()
        {
            _playerInput.Player.Move.performed += MoveTo;
            _playerInput.Player.Move.canceled += MoveTo;
            _playerInput.Player.Shoot.started += Shoot;
        }

        private void UnsubscribeEvents()
        {
            _playerInput.Player.Move.performed -= MoveTo;
            _playerInput.Player.Move.canceled -= MoveTo;
            _playerInput.Player.Shoot.started -= Shoot;
        }

        private void MoveTo(InputAction.CallbackContext ctx)
        {
            Vector2 moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            Moved?.Invoke(moveDirection);
        }

        private void Shoot(InputAction.CallbackContext ctx) =>
            Shot?.Invoke();

        public class Factory : PlaceholderFactory<IPlatformInputService, KeyboardMouseInputType>
        {
        }
    }
}