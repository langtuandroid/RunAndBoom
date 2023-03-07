using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HealthState HealthState;
        public WorldData WorldData;
        public WeaponsData WeaponsData;
        public LevelStats CurrentLevelStats { get; private set; }
        public List<LevelStats> LevelStats { get; private set; }

        public PlayerProgress(string initialLevel)
        {
            HealthState = new HealthState();
            WorldData = new WorldData(initialLevel);
            WeaponsData = new WeaponsData();
            CurrentLevelStats = new LevelStats(initialLevel);
        }


        public void AddNewLevelStats(LevelStats levelStats) =>
            LevelStats.Add(levelStats);
    }
}