using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Upgrades;
using CodeBase.StaticData.Weapons;
using UnityEngine.Serialization;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class WeaponsData
    {
        private const int InitialRpgAmmoCount = 8;
        private const int InitialRlAmmoCount = 12;
        private const int InitialMortarAmmoCount = 6;

        private List<HeroWeaponTypeId> _typeIds = DataExtensions.GetValues<HeroWeaponTypeId>().ToList();
       public List<WeaponData> WeaponData;
        public WeaponsAmmoData WeaponsAmmoData;
        public UpgradesData UpgradesData;
        public HeroWeaponTypeId CurrentHeroWeaponTypeId;

        public event Action<HeroWeaponTypeId> SetAvailable;
        public event Action CurrentWeaponChanged;

        public WeaponsData(Scene scene)
        {
            WeaponData = new List<WeaponData>(_typeIds.Count);
            WeaponsAmmoData = new WeaponsAmmoData(WeaponData, scene);
            UpgradesData = new UpgradesData();
            FillAvailableWeapons();
            CurrentHeroWeaponTypeId = WeaponData.First(x => x.IsAvailable).WeaponTypeId;
        }

        private void FillAvailableWeapons()
        {
            WeaponData.Add(new WeaponData(HeroWeaponTypeId.GrenadeLauncher, true));
            // WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.RPG, false));
            WeaponData.Add(new WeaponData(HeroWeaponTypeId.RPG, true));
            // WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.RocketLauncher, false));
            WeaponData.Add(new WeaponData(HeroWeaponTypeId.RocketLauncher, true));
            // WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.Mortar, false));
            WeaponData.Add(new WeaponData(HeroWeaponTypeId.Mortar, true));

            SetAvailableWeapons();
        }

        private void SetAvailableWeapons()
        {
            foreach (WeaponData weaponData in WeaponData)
                if (weaponData.IsAvailable)
                    SetAvailable?.Invoke(weaponData.WeaponTypeId);
        }

        public void SetCurrentWeapon(HeroWeaponTypeId typeId)
        {
            CurrentHeroWeaponTypeId = typeId;
            WeaponsAmmoData.SetCurrentWeapon(typeId);
            CurrentWeaponChanged?.Invoke();
        }

        public void SetAvailableWeapon(HeroWeaponTypeId typeId)
        {
            WeaponData.First(x => x.WeaponTypeId == typeId).SetWeaponAvailable();

            switch (typeId)
            {
                case HeroWeaponTypeId.RPG:
                    WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.RPG, InitialRpgAmmoCount);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.RocketLauncher, InitialRlAmmoCount);
                    break;
                case HeroWeaponTypeId.Mortar:
                    WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.Mortar, InitialMortarAmmoCount);
                    break;
            }

            SetAvailable?.Invoke(typeId);
        }
    }
}