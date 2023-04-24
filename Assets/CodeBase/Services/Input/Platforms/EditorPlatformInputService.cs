using System;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public class EditorPlatformInputService : PlatformInputService
    {
        private readonly IPlatformInputService _platformInputService1;
        private readonly IPlatformInputService _platformInputService2;

        public override event Action<Vector2> Moved;
        public override event Action Shot;
        public override event Action ChoseWeapon1;
        public override event Action ChoseWeapon2;
        public override event Action ChoseWeapon3;
        public override event Action ChoseWeapon4;

        public EditorPlatformInputService(IPlatformInputService platformInputService1,
            IPlatformInputService platformInputService2)
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
            _platformInputService1.Shot += ShootTo;
            _platformInputService1.ChoseWeapon1 += ChooseWeapon1;
            _platformInputService1.ChoseWeapon2 += ChooseWeapon2;
            _platformInputService1.ChoseWeapon3 += ChooseWeapon3;
            _platformInputService1.ChoseWeapon4 += ChooseWeapon4;

            _platformInputService2.Moved += MoveTo;
            _platformInputService2.Shot += ShootTo;
            _platformInputService2.ChoseWeapon1 += ChooseWeapon1;
            _platformInputService2.ChoseWeapon2 += ChooseWeapon2;
            _platformInputService2.ChoseWeapon3 += ChooseWeapon3;
            _platformInputService2.ChoseWeapon4 += ChooseWeapon4;
        }

        protected override void UnsubscribeEvents()
        {
            _platformInputService1.Moved -= MoveTo;
            _platformInputService1.Shot -= ShootTo;
            _platformInputService1.ChoseWeapon1 -= ChooseWeapon1;
            _platformInputService1.ChoseWeapon2 -= ChooseWeapon2;
            _platformInputService1.ChoseWeapon3 -= ChooseWeapon3;
            _platformInputService1.ChoseWeapon4 -= ChooseWeapon4;

            _platformInputService2.Moved -= MoveTo;
            _platformInputService2.Shot -= ShootTo;
            _platformInputService2.ChoseWeapon1 -= ChooseWeapon1;
            _platformInputService2.ChoseWeapon2 -= ChooseWeapon2;
            _platformInputService2.ChoseWeapon3 -= ChooseWeapon3;
            _platformInputService2.ChoseWeapon4 -= ChooseWeapon4;
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