using System;
using UnityEngine;

namespace CodeBase.Services.Input.Platforms
{
    public interface IPlatformInputService : IService
    {
        public event Action<Vector2> Moved;
        public event Action Shot;
        public event Action ChoseWeapon1;
        public event Action ChoseWeapon2;
        public event Action ChoseWeapon3;
        public event Action ChoseWeapon4;
    }
}