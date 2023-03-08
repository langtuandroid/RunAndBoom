using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Weapon;

namespace CodeBase.Data
{
    [Serializable]
    public class WeaponsData
    {
        private List<HeroWeaponTypeId> _typeIds = Enum.GetValues(typeof(HeroWeaponTypeId)).Cast<HeroWeaponTypeId>().ToList();

        public HeroWeaponTypeId CurrentHeroWeaponTypeId { get; private set; }
        public Dictionary<HeroWeaponTypeId, bool> AvailableWeapons { get; private set; }
        public Dictionary<HeroWeaponTypeId, int> WeaponsAmmo { get; private set; }
        public Dictionary<HeroWeaponTypeId, int> WeaponsBarrels { get; private set; }

        public event Action HeroWeaponChanged;
        public event Action<int> GrenadeLauncherAmmoChanged;
        public event Action<int> RpgAmmoChanged;
        public event Action<int> RocketLauncherAmmoChanged;
        public event Action<int> MortarAmmoChanged;

        public WeaponsData()
        {
            FillAmmo();
            FillWeaponsBarrels();
            FillAvailableWeaponDates();
            CurrentHeroWeaponTypeId = AvailableWeapons.First(x => x.Value).Key;
        }

        private void FillAmmo()
        {
            WeaponsAmmo = new Dictionary<HeroWeaponTypeId, int>();
            WeaponsAmmo[HeroWeaponTypeId.GrenadeLauncher] = 10;
            AmmoChanged(HeroWeaponTypeId.GrenadeLauncher);
            WeaponsAmmo[HeroWeaponTypeId.RPG] = 10;
            AmmoChanged(HeroWeaponTypeId.RPG);
            WeaponsAmmo[HeroWeaponTypeId.RocketLauncher] = 10;
            AmmoChanged(HeroWeaponTypeId.RocketLauncher);
            WeaponsAmmo[HeroWeaponTypeId.Mortar] = 10;
            AmmoChanged(HeroWeaponTypeId.Mortar);
        }

        private void FillWeaponsBarrels()
        {
            WeaponsBarrels = new Dictionary<HeroWeaponTypeId, int>();
            WeaponsBarrels[HeroWeaponTypeId.GrenadeLauncher] = 1;
            WeaponsBarrels[HeroWeaponTypeId.RPG] = 1;
            WeaponsBarrels[HeroWeaponTypeId.RocketLauncher] = 3;
            WeaponsBarrels[HeroWeaponTypeId.Mortar] = 1;
        }

        private void FillAvailableWeaponDates()
        {
            AvailableWeapons = new Dictionary<HeroWeaponTypeId, bool>();

            foreach (HeroWeaponTypeId typeId in _typeIds)
            {
                if (typeId == HeroWeaponTypeId.GrenadeLauncher)
                    AvailableWeapons.Add(typeId, true);
                else
                    AvailableWeapons.Add(typeId, false);
            }
        }

        private void AddAmmo(HeroWeaponTypeId typeId, int ammo)
        {
            WeaponsAmmo[typeId] += ammo;
            AmmoChanged(typeId);
        }

        public void SetCurrentWeapon(HeroWeaponTypeId typeId)
        {
            CurrentHeroWeaponTypeId = typeId;
            HeroWeaponChanged?.Invoke();
        }

        public bool IsWeaponAvailable(HeroWeaponTypeId typeId) =>
            AvailableWeapons[typeId];

        public void SetWeaponAvailable(HeroWeaponTypeId typeId) =>
            AvailableWeapons[typeId] = true;

        public bool IsAmmoAvailable() =>
            WeaponsBarrels[CurrentHeroWeaponTypeId] <= WeaponsAmmo[CurrentHeroWeaponTypeId];

        public void ReduceAmmo()
        {
            WeaponsAmmo[CurrentHeroWeaponTypeId] -= WeaponsBarrels[CurrentHeroWeaponTypeId];
            AmmoChanged(CurrentHeroWeaponTypeId);
        }

        private void AmmoChanged(HeroWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    GrenadeLauncherAmmoChanged?.Invoke(WeaponsAmmo[CurrentHeroWeaponTypeId]);
                    break;
                case HeroWeaponTypeId.RPG:
                    RpgAmmoChanged?.Invoke(WeaponsAmmo[CurrentHeroWeaponTypeId]);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    RocketLauncherAmmoChanged?.Invoke(WeaponsAmmo[CurrentHeroWeaponTypeId]);
                    break;
                case HeroWeaponTypeId.Mortar:
                    MortarAmmoChanged?.Invoke(WeaponsAmmo[CurrentHeroWeaponTypeId]);
                    break;
            }
        }
    }
}