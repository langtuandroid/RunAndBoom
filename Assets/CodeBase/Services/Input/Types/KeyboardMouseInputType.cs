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
        public event Action ChoseWeapon1;
        public event Action ChoseWeapon2;
        public event Action ChoseWeapon3;
        public event Action ChoseWeapon4;

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
            _playerInput.Player.Shoot.started += Shoot;
            _playerInput.Player.ChooseWeapon1.started += ChooseWeapon1;
            _playerInput.Player.ChooseWeapon2.started += ChooseWeapon2;
            _playerInput.Player.ChooseWeapon3.started += ChooseWeapon3;
            _playerInput.Player.ChooseWeapon4.started += ChooseWeapon4;
        }

        private void UnsubscribeEvents()
        {
            _playerInput.Player.Move.performed -= MoveTo;
            _playerInput.Player.Move.canceled -= MoveTo;
            _playerInput.Player.Shoot.started -= Shoot;
            _playerInput.Player.ChooseWeapon1.started -= ChooseWeapon1;
            _playerInput.Player.ChooseWeapon2.started -= ChooseWeapon2;
            _playerInput.Player.ChooseWeapon3.started -= ChooseWeapon3;
            _playerInput.Player.ChooseWeapon4.started -= ChooseWeapon4;
        }

        private void MoveTo(InputAction.CallbackContext ctx)
        {
            Vector2 moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            Moved?.Invoke(moveDirection);
        }

        private void Shoot(InputAction.CallbackContext ctx) =>
            Shot?.Invoke();

        private void ChooseWeapon1(InputAction.CallbackContext ctx) =>
            ChoseWeapon1?.Invoke();

        private void ChooseWeapon2(InputAction.CallbackContext ctx) =>
            ChoseWeapon2?.Invoke();

        private void ChooseWeapon3(InputAction.CallbackContext ctx) =>
            ChoseWeapon3?.Invoke();

        private void ChooseWeapon4(InputAction.CallbackContext ctx) =>
            ChoseWeapon4?.Invoke();

        public class Factory : PlaceholderFactory<IPlatformInputService, KeyboardMouseInputType>
        {
        }
    }
}