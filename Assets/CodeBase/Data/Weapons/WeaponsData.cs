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
        public WeaponsAmmo WeaponsAmmo { get; private set; }
        public WeaponsUpgrades WeaponsUpgrades { get; private set; }
        public HeroWeaponTypeId CurrentHeroWeaponTypeId { get; private set; }
        public Dictionary<HeroWeaponTypeId, bool> AvailableWeapons { get; private set; }

        public WeaponsData()
        {
            WeaponsAmmo = new WeaponsAmmo();
            WeaponsUpgrades = new WeaponsUpgrades();
            FillAvailableWeapons();
            CurrentHeroWeaponTypeId = AvailableWeapons.First(x => x.Value).Key;
        }

        private void FillAvailableWeapons()
        {
            List<HeroWeaponTypeId> typeIds = Enum.GetValues(typeof(HeroWeaponTypeId)).Cast<HeroWeaponTypeId>().ToList();
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
            WeaponsAmmo.SetCurrentWeapon(typeId);
        }

        public void SetAvailableWeapons(Dictionary<HeroWeaponTypeId, bool> availableWeapons) =>
            AvailableWeapons = availableWeapons;
    }
}