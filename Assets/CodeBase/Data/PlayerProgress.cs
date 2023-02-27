using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Weapon;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [FormerlySerializedAs("CurrentWeaponTypeId")]
        public HeroWeaponTypeId currentHeroWeaponTypeId;

        public Dictionary<HeroWeaponTypeId, bool> AvailableWeapons { get; private set; }
        public LevelStats CurrentLevelStats { get; private set; }
        public List<LevelStats> LevelStats { get; private set; }
        public int MaxHP { get; private set; }

        List<HeroWeaponTypeId> typeIds = Enum.GetValues(typeof(HeroWeaponTypeId)).Cast<HeroWeaponTypeId>().ToList();

        public PlayerProgress()
        {
            FillAvailableWeaponDates();
            CurrentLevelStats = new LevelStats();
            currentHeroWeaponTypeId = AvailableWeapons.First(x => x.Value).Key;
        }

        private void FillAvailableWeaponDates()
        {
            AvailableWeapons = new Dictionary<HeroWeaponTypeId, bool>();

            foreach (HeroWeaponTypeId typeId in typeIds)
            {
                if (typeId == HeroWeaponTypeId.GrenadeLauncher)
                    AvailableWeapons.Add(typeId, true);
                else
                    AvailableWeapons.Add(typeId, false);
            }
        }

        public void SetAvailableWeapons(Dictionary<HeroWeaponTypeId, bool> availableWeaponDates)
        {
            AvailableWeapons.Clear();
            AvailableWeapons = availableWeaponDates;
        }

        public void SetMaxHP(int value) =>
            MaxHP = value;

        public void AddNewLevelStats(LevelStats levelStats) =>
            LevelStats.Add(levelStats);
    }
}