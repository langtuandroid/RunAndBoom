using System;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class WeaponData : ItemData
    {
        public HeroWeaponTypeId WeaponTypeId { get; private set; }
        public bool IsAvailable { get; private set; }

        public WeaponData(HeroWeaponTypeId weaponTypeId, bool isAvailable)
        {
            WeaponTypeId = weaponTypeId;
            IsAvailable = isAvailable;
        }

        public bool IsWeaponAvailable() =>
            IsAvailable;

        public void SetWeaponAvailable() =>
            IsAvailable = true;
    }
}