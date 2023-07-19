using System;
using CodeBase.Data.Perks;
using CodeBase.Data.Settings;
using CodeBase.Data.Stats;
using CodeBase.Data.Weapons;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public SettingsData SettingsData;
        public HealthState HealthState;
        public WorldData WorldData;
        public WeaponsData WeaponsData;
        public PerksData PerksData;
        public AllStats AllStats;

        public PlayerProgress(Scene initialLevel, Language language, int targetPlayTime, int totalEnemies)
        {
            SettingsData = new SettingsData(language);
            HealthState = new HealthState();
            AllStats = new AllStats();

            if (AllStats.CurrentLevelStats == null)
                AllStats.CurrentLevelStats = new LevelStats(initialLevel, targetPlayTime, totalEnemies);

            WorldData = new WorldData(AllStats.CurrentLevelStats.Scene.ToString());
            PerksData = new PerksData();
            AllStats.LevelStats = new SceneDataDictionary();
            WeaponsData = new WeaponsData(AllStats.CurrentLevelStats.Scene);
        }
    }
}