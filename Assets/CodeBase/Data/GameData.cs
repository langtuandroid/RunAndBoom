using System;
using CodeBase.Data.Progress;
using CodeBase.Data.Settings;

namespace CodeBase.Data
{
    [Serializable]
    public class GameData
    {
        public ProgressData ProgressData;
        public SettingsData SettingsData;

        public GameData(SceneId initialLevel, int targetPlayTime, int totalEnemies, bool isHardMode, Language language)
        {
            ProgressData = new ProgressData(initialLevel, targetPlayTime, totalEnemies, isHardMode);
            SettingsData = new SettingsData(language);
        }
    }
}