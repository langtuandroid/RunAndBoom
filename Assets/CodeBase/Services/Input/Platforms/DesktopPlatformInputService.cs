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
        public override event Action ChoseWeapon1;
        public override event Action ChoseWeapon2;
        public override event Action ChoseWeapon3;
        public override event Action ChoseWeapon4;

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
            _keyboardMouseInputType.Shot += ShootTo;
            _keyboardMouseInputType.ChoseWeapon1 += ChooseWeapon1;
            _keyboardMouseInputType.ChoseWeapon2 += ChooseWeapon2;
            _keyboardMouseInputType.ChoseWeapon3 += ChooseWeapon3;
            _keyboardMouseInputType.ChoseWeapon4 += ChooseWeapon4;
        }

        protected override void UnsubscribeEvents()
        {
            _keyboardMouseInputType.Moved -= MoveTo;
            _keyboardMouseInputType.Shot -= ShootTo;
            _keyboardMouseInputType.ChoseWeapon1 -= ChooseWeapon1;
            _keyboardMouseInputType.ChoseWeapon2 -= ChooseWeapon2;
            _keyboardMouseInputType.ChoseWeapon3 -= ChooseWeapon3;
            _keyboardMouseInputType.ChoseWeapon4 -= ChooseWeapon4;
        }


        protected override void MoveTo(Vector2 direction) =>
            Moved?.Invoke(direction);

        protected override void ShootTo() =>
            Shot?.Invoke();

        private void ChooseWeapon1() =>
            ChoseWeapon1?.Invoke();

        private void ChooseWeapon2() =>
            ChoseWeapon2?.Invoke();

        private void ChooseWeapon3() =>
            ChoseWeapon3?.Invoke();

        private void ChooseWeapon4() =>
            ChoseWeapon4?.Invoke();
    }
}