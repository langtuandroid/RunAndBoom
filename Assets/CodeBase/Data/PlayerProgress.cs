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
        public Stats.Stats Stats;

        public PlayerProgress(Scene initialLevel, Language language)
        {
            SettingsData = new SettingsData(language);
            HealthState = new HealthState();
            Stats = new Stats.Stats();

            if (Stats.CurrentLevelStats == null)
                Stats.CurrentLevelStats = new LevelStats(initialLevel);

            WorldData = new WorldData(Stats.CurrentLevelStats.Scene.ToString());
            PerksData = new PerksData();
            Stats.LevelStats = new SceneDataDictionary();
            WeaponsData = new WeaponsData(Stats.CurrentLevelStats.Scene);
        }
    }
}