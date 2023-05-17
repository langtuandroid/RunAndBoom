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
        public Dictionary<Scene, LevelStats> LevelStats { get; private set; }

        public PlayerProgress(Scene initialLevel)
        {
            HealthState = new HealthState();
            WorldData = new WorldData(initialLevel.ToString());
            WeaponsData = new WeaponsData();
            PerksData = new PerksData();
            CurrentLevelStats = new LevelStats(initialLevel);
            LevelStats = new Dictionary<Scene, LevelStats>();
        }

        public void StartNewLevel(Scene scene)
        {
            LevelStats[CurrentLevelStats.Scene] = CurrentLevelStats;
            CurrentLevelStats = new LevelStats(scene);
        }

        public void SetWeaponData(WeaponsData weaponsData) =>
            WeaponsData = weaponsData;
    }
}