using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Weapon;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WeaponTypeId CurrentWeaponTypeId;

        public Dictionary<WeaponTypeId, bool> AvailableWeapons { get; private set; }
        public LevelStats CurrentLevelStats { get; private set; }
        public List<LevelStats> LevelStats { get; private set; }
        public int MaxHP { get; private set; }

        List<WeaponTypeId> typeIds = Enum.GetValues(typeof(WeaponTypeId)).Cast<WeaponTypeId>().ToList();

        public PlayerProgress()
        {
            FillAvailableWeaponDates();
            CurrentLevelStats = new LevelStats();
            CurrentWeaponTypeId = AvailableWeapons.First(x => x.Value).Key;
        }

        private void FillAvailableWeaponDates()
        {
            AvailableWeapons = new Dictionary<WeaponTypeId, bool>();

            foreach (WeaponTypeId typeId in typeIds)
            {
                if (typeId == WeaponTypeId.RPG)
                    AvailableWeapons.Add(typeId, true);
                else
                    AvailableWeapons.Add(typeId, false);
            }
        }

        public void SetAvailableWeapons(Dictionary<WeaponTypeId, bool> availableWeaponDates)
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