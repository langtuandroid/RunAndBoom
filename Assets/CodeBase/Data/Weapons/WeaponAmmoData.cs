using System;
using System.Collections.Generic;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class WeaponsAmmoData
    {
        private HeroWeaponTypeId _currentHeroWeaponTypeId;

        public Dictionary<HeroWeaponTypeId, int> Ammo { get; private set; }
        public Dictionary<HeroWeaponTypeId, int> Barrels { get; private set; }

        public event Action<int> GrenadeLauncherAmmoChanged;
        public event Action<int> RpgAmmoChanged;
        public event Action<int> RocketLauncherAmmoChanged;
        public event Action<int> MortarAmmoChanged;

        public WeaponsAmmoData()
        {
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
            Ammo[HeroWeaponTypeId.GrenadeLauncher] = 10;
            AmmoChanged(HeroWeaponTypeId.GrenadeLauncher);
            Ammo[HeroWeaponTypeId.RPG] = 10;
            AmmoChanged(HeroWeaponTypeId.RPG);
            Ammo[HeroWeaponTypeId.RocketLauncher] = 10;
            AmmoChanged(HeroWeaponTypeId.RocketLauncher);
            Ammo[HeroWeaponTypeId.Mortar] = 10;
            AmmoChanged(HeroWeaponTypeId.Mortar);
        }

        private void AddAmmo(HeroWeaponTypeId typeId, int ammo)
        {
            Ammo[typeId] += ammo;
            AmmoChanged(typeId);
        }

        public bool IsAmmoAvailable() =>
            Barrels[_currentHeroWeaponTypeId] <= Ammo[_currentHeroWeaponTypeId];

        public void ReduceAmmo()
        {
            Ammo[_currentHeroWeaponTypeId] -= Barrels[_currentHeroWeaponTypeId];
            AmmoChanged(_currentHeroWeaponTypeId);
        }

        private void AmmoChanged(HeroWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    GrenadeLauncherAmmoChanged?.Invoke(Ammo[_currentHeroWeaponTypeId]);
                    break;
                case HeroWeaponTypeId.RPG:
                    RpgAmmoChanged?.Invoke(Ammo[_currentHeroWeaponTypeId]);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    RocketLauncherAmmoChanged?.Invoke(Ammo[_currentHeroWeaponTypeId]);
                    break;
                case HeroWeaponTypeId.Mortar:
                    MortarAmmoChanged?.Invoke(Ammo[_currentHeroWeaponTypeId]);
                    break;
            }
        }
    }
}