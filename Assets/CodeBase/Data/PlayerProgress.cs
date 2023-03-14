using System;
using System.Collections.Generic;
using CodeBase.Data.Perks;
using CodeBase.Data.Weapons;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState { get; private set; }
        public WorldData WorldData { get; private set; }
        public WeaponsData WeaponsData { get; private set; }
        public PerksData PerksData { get; private set; }
        public LevelStats CurrentLevelStats { get; private set; }
        public List<LevelStats> LevelStats { get; private set; }

        public PlayerProgress(string initialLevel)
        {
            HealthState = new HealthState();
            WorldData = new WorldData(initialLevel);
            WeaponsData = new WeaponsData();
            PerksData = new PerksData();
            CurrentLevelStats = new LevelStats(initialLevel);
        }

        public void AddNewLevelStats(LevelStats levelStats) =>
            LevelStats.Add(levelStats);

        public void SetWeaponData(WeaponsData weaponsData) =>
            WeaponsData = weaponsData;
    }
}