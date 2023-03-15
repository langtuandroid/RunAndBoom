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
        public WeaponsAmmoData WeaponsAmmoData { get; private set; }
        public WeaponUpgradesData WeaponUpgradesData { get; private set; }
        public HeroWeaponTypeId CurrentHeroWeaponTypeId { get; private set; }
        public Dictionary<HeroWeaponTypeId, bool> AvailableWeapons { get; private set; }

        public WeaponsData()
        {
            WeaponsAmmoData = new WeaponsAmmoData();
            WeaponUpgradesData = new WeaponUpgradesData();
            FillAvailableWeapons();
            CurrentHeroWeaponTypeId = AvailableWeapons.First(x => x.Value).Key;
        }

        private void FillAvailableWeapons()
        {
            List<HeroWeaponTypeId> typeIds = DataExtensions.GetValues<HeroWeaponTypeId>().ToList();
            AvailableWeapons = new Dictionary<HeroWeaponTypeId, bool>();

            foreach (HeroWeaponTypeId typeId in typeIds)
            {
                if (typeId == HeroWeaponTypeId.GrenadeLauncher)
                    AvailableWeapons.Add(typeId, true);
                else
                    AvailableWeapons.Add(typeId, true);
            }
        }

        public void SetCurrentWeapon(HeroWeaponTypeId typeId)
        {
            CurrentHeroWeaponTypeId = typeId;
            WeaponsAmmoData.SetCurrentWeapon(typeId);
        }

        public void SetAvailableWeapons(Dictionary<HeroWeaponTypeId, bool> availableWeapons) =>
            AvailableWeapons = availableWeapons;
    }
}