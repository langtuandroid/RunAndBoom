using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Weapons;
using UnityEngine.Rendering;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class WeaponsAmmoData : ItemData
    {
        private HeroWeaponTypeId _currentHeroWeaponTypeId;
        private List<WeaponData> _weaponDatas;

        public AmunitionDataDictionary Amunition = new()
        {
            Dictionary = new SerializedDictionary<HeroWeaponTypeId, int>()
            {
                { HeroWeaponTypeId.GrenadeLauncher, 100 },
                { HeroWeaponTypeId.RPG, 0 },
                { HeroWeaponTypeId.RocketLauncher, 0 },
                { HeroWeaponTypeId.Mortar, 0 },
            }
        };

        public BarrelDataDictionary Barrels = new();

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
            Barrels.Dictionary[HeroWeaponTypeId.GrenadeLauncher] = 1;
            Barrels.Dictionary[HeroWeaponTypeId.RPG] = 1;
            Barrels.Dictionary[HeroWeaponTypeId.RocketLauncher] = 3;
            Barrels.Dictionary[HeroWeaponTypeId.Mortar] = 1;
        }

        private void FillAmmo()
        {
            // Amunition.Dictionary[HeroWeaponTypeId.GrenadeLauncher] = 100;
            AmmoChanged(HeroWeaponTypeId.GrenadeLauncher);
            // Amunition.Dictionary[HeroWeaponTypeId.RPG] = 0;
            AmmoChanged(HeroWeaponTypeId.RPG);
            // Amunition.Dictionary[HeroWeaponTypeId.RocketLauncher] = 0;
            AmmoChanged(HeroWeaponTypeId.RocketLauncher);
            // Amunition.Dictionary[HeroWeaponTypeId.Mortar] = 0;
            AmmoChanged(HeroWeaponTypeId.Mortar);
        }

        public void AddAmmo(HeroWeaponTypeId typeId, int ammo)
        {
            int current = Amunition.Dictionary[typeId];
            int result = current + ammo;
            Amunition.Dictionary[typeId] = result;
            AmmoChanged(typeId);
        }

        public bool IsAmmoAvailable() =>
            Barrels.Dictionary[_currentHeroWeaponTypeId] <= Amunition.Dictionary[_currentHeroWeaponTypeId];

        public void ReduceAmmo()
        {
            Amunition.Dictionary[_currentHeroWeaponTypeId] -= Barrels.Dictionary[_currentHeroWeaponTypeId];
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
                    GrenadeLauncherAmmoChanged?.Invoke(Amunition.Dictionary[typeId]);
                    break;
                case HeroWeaponTypeId.RPG:
                    RpgAmmoChanged?.Invoke(Amunition.Dictionary[typeId]);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    RocketLauncherAmmoChanged?.Invoke(Amunition.Dictionary[typeId]);
                    break;
                case HeroWeaponTypeId.Mortar:
                    MortarAmmoChanged?.Invoke(Amunition.Dictionary[typeId]);
                    break;
            }
        }
    }
}