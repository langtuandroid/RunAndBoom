using System;
using CodeBase.Data.Perks;
using CodeBase.Data.Settings;
using CodeBase.Data.Weapons;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public SettingsData SettingsData { get; private set; }
        public HealthState HealthState { get; private set; }
        public WorldData WorldData { get; private set; }
        public WeaponsData WeaponsData { get; private set; }
        public PerksData PerksData { get; private set; }
        public LevelStats CurrentLevelStats { get; private set; }

        public SceneDataDictionary LevelStats { get; private set; }

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

        public void SetWeaponData(WeaponsData weaponsData) =>
            WeaponsData = weaponsData;
    }
}