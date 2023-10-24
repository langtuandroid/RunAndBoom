using CodeBase.Data.Progress;
using CodeBase.Data.Settings;

namespace CodeBase.Services.PersistentProgress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        // public GameData GameData { get; private set; }
        public ProgressData ProgressData { get; private set; }
        public SettingsData SettingsData { get; private set; }
        public string CurrentError { get; set; }

        // public void ClearGameData() =>
        //     GameData = null;
        public void ClearProgressData() =>
            ProgressData = null;

        // public void SetGameData(GameData gameData) =>
        // GameData = gameData;

        public void SetProgressData(ProgressData progressData) =>
            ProgressData = progressData;

        public void SetSettingsData(SettingsData settingsData) =>
            SettingsData = settingsData;
    }
}