using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Weapon;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState;
        public WorldData WorldData;

        public HeroWeaponTypeId CurrentHeroWeaponTypeId;

        private List<HeroWeaponTypeId> _typeIds = Enum.GetValues(typeof(HeroWeaponTypeId)).Cast<HeroWeaponTypeId>().ToList();

        public Dictionary<HeroWeaponTypeId, bool> AvailableWeapons { get; private set; }
        public LevelStats CurrentLevelStats { get; private set; }
        public List<LevelStats> LevelStats { get; private set; }

        public PlayerProgress(string initialLevel)
        {
            HealthState = new HealthState();
            WorldData = new WorldData(initialLevel);
            FillAvailableWeaponDates();
            CurrentLevelStats = new LevelStats(initialLevel);
            CurrentHeroWeaponTypeId = AvailableWeapons.First(x => x.Value).Key;
        }

        private void FillAvailableWeaponDates()
        {
            AvailableWeapons = new Dictionary<HeroWeaponTypeId, bool>();

            foreach (HeroWeaponTypeId typeId in _typeIds)
            {
                if (typeId == HeroWeaponTypeId.RocketLauncher)
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

        public void AddNewLevelStats(LevelStats levelStats) =>
            LevelStats.Add(levelStats);
    }
}