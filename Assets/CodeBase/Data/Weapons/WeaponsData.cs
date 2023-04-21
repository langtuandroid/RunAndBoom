using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data.Upgrades;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Data.Weapons
{
    [Serializable]
    public class WeaponsData
    {
        List<HeroWeaponTypeId> _typeIds = DataExtensions.GetValues<HeroWeaponTypeId>().ToList();

        public List<WeaponData> WeaponDatas { get; private set; }
        public WeaponsAmmoData WeaponsAmmoData { get; private set; }
        public UpgradesData UpgradesData { get; private set; }
        public HeroWeaponTypeId CurrentHeroWeaponTypeId { get; private set; }

        public WeaponsData()
        {
            WeaponDatas = new List<WeaponData>(_typeIds.Count);
            WeaponsAmmoData = new WeaponsAmmoData();
            UpgradesData = new UpgradesData();
            FillAvailableWeapons();
            CurrentHeroWeaponTypeId = WeaponDatas.First(x => x.IsAvailable).WeaponTypeId;
        }

        private void FillAvailableWeapons()
        {
            WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.GrenadeLauncher, true));
            WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.RPG, true));
            WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.RocketLauncher, true));
            WeaponDatas.Add(new WeaponData(HeroWeaponTypeId.Mortar, true));
        }

        public void SetCurrentWeapon(HeroWeaponTypeId typeId)
        {
            CurrentHeroWeaponTypeId = typeId;
            WeaponsAmmoData.SetCurrentWeapon(typeId);
        }

        public void SetAvailableWeapon(HeroWeaponTypeId typeId) =>
            WeaponDatas.First(x => x.WeaponTypeId == typeId).SetWeaponAvailable();
    }
}