using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class WeaponsAmmoData : ItemData
    {
        private HeroWeaponTypeId _currentHeroWeaponTypeId;
        private List<WeaponData> _weaponDatas;

        public Dictionary<HeroWeaponTypeId, int> Ammo { get; private set; }
        public Dictionary<HeroWeaponTypeId, int> Barrels { get; private set; }

        public event Action<int> GrenadeLauncherAmmoChanged;
        public event Action<int> RpgAmmoChanged;
        public event Action<int> RocketLauncherAmmoChanged;
        public event Action<int> MortarAmmoChanged;

        public WeaponsAmmoData(List<WeaponData> weaponDatas)
        {
            _weaponDatas = weaponDatas;
            FillAmmo();
            FillWeaponsBarrels();
        }

        public void SetCurrentWeapon(HeroWeaponTypeId typeId) =>
            _currentHeroWeaponTypeId = typeId;

        private void FillWeaponsBarrels()
        {
            Barrels = new Dictionary<HeroWeaponTypeId, int>();
            Barrels[HeroWeaponTypeId.GrenadeLauncher] = 1;
            Barrels[HeroWeaponTypeId.RPG] = 1;
            Barrels[HeroWeaponTypeId.RocketLauncher] = 3;
            Barrels[HeroWeaponTypeId.Mortar] = 1;
        }

        private void FillAmmo()
        {
            Ammo = new Dictionary<HeroWeaponTypeId, int>();
            Ammo[HeroWeaponTypeId.GrenadeLauncher] = 100;
            // Ammo[HeroWeaponTypeId.GrenadeLauncher] = 10;
            AmmoChanged(HeroWeaponTypeId.GrenadeLauncher);
            Ammo[HeroWeaponTypeId.RPG] = 0;
            AmmoChanged(HeroWeaponTypeId.RPG);
            Ammo[HeroWeaponTypeId.RocketLauncher] = 0;
            AmmoChanged(HeroWeaponTypeId.RocketLauncher);
            Ammo[HeroWeaponTypeId.Mortar] = 0;
            AmmoChanged(HeroWeaponTypeId.Mortar);
        }

        public void AddAmmo(HeroWeaponTypeId typeId, int ammo)
        {
            int current = Ammo[typeId];
            int result = current + ammo;
            Ammo[typeId] = result;
            AmmoChanged(typeId);
        }

        public bool IsAmmoAvailable() =>
            Barrels[_currentHeroWeaponTypeId] <= Ammo[_currentHeroWeaponTypeId];

        public void ReduceAmmo()
        {
            Ammo[_currentHeroWeaponTypeId] -= Barrels[_currentHeroWeaponTypeId];
            AmmoChanged(_currentHeroWeaponTypeId);
        }

        public void TryChangeAmmo(HeroWeaponTypeId typeId)
        {
            if (_weaponDatas.First(x => x.WeaponTypeId == typeId).IsAvailable)
                AmmoChanged(typeId);
        }

        public void AmmoChanged(HeroWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    GrenadeLauncherAmmoChanged?.Invoke(Ammo[typeId]);
                    break;
                case HeroWeaponTypeId.RPG:
                    RpgAmmoChanged?.Invoke(Ammo[typeId]);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    RocketLauncherAmmoChanged?.Invoke(Ammo[typeId]);
                    break;
                case HeroWeaponTypeId.Mortar:
                    MortarAmmoChanged?.Invoke(Ammo[typeId]);
                    break;
            }
        }
    }
}