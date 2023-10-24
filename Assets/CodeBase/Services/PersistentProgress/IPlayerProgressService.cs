using CodeBase.Data.Progress;
using CodeBase.Data.Settings;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPlayerProgressService : IService
    {
        // public GameData GameData { get; }
        public ProgressData ProgressData { get; }
        public SettingsData SettingsData { get; }

        string CurrentError { get; set; }

        void ClearProgressData();

        // void ClearGameData();
        // void SetGameData(GameData gameData);
        void SetProgressData(ProgressData progressData);
        void SetSettingsData(SettingsData settingsData);
    }
}