using System;
using CodeBase.Data.Perks;
using CodeBase.Data.Settings;
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
        public LevelStats CurrentLevelStats;
        public SceneDataDictionary LevelStats;

        public PlayerProgress(Scene initialLevel, Language language)
        {
            SettingsData = new SettingsData(language);
            HealthState = new HealthState();
            WorldData = new WorldData(initialLevel.ToString());
            WeaponsData = new WeaponsData();
            PerksData = new PerksData();
            CurrentLevelStats = new LevelStats(initialLevel);
            LevelStats = new SceneDataDictionary();
        }

        public void StartNewLevel(Scene scene)
        {
            LevelStats.Dictionary[CurrentLevelStats.Scene] = CurrentLevelStats;
            CurrentLevelStats = new LevelStats(scene);
        }
    }
}