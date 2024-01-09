using System;
using CodeBase.Data.Progress.Perks;
using CodeBase.Data.Progress.Stats;
using CodeBase.Data.Progress.Weapons;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class ProgressData
    {
        public HealthState HealthState;
        public WorldData WorldData;
        public WeaponsData WeaponsData;
        public PerksData PerksData;
        public AllStats AllStats;
        public bool IsAsianMode;

        public ProgressData(SceneId initialLevel, int targetPlayTime, int maxStarsScore, int totalEnemies, bool isAsianMode)
        {
            AllStats = new AllStats();

            if (AllStats.CurrentLevelStats == null)
                AllStats.CurrentLevelStats = new LevelStats(initialLevel, targetPlayTime,  maxStarsScore, totalEnemies);

            HealthState = new HealthState(AllStats.CurrentLevelStats.SceneId);
            WorldData = new WorldData(AllStats.CurrentLevelStats.SceneId.ToString());
            PerksData = new PerksData();
            AllStats.LevelsStats = new SceneDataDictionary();
            WeaponsData = new WeaponsData(AllStats.CurrentLevelStats.SceneId, isAsianMode);
            IsAsianMode = isAsianMode;
        }
    }
}