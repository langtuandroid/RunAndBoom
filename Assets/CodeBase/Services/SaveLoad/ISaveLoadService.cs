using CodeBase.Data.Progress;
using CodeBase.Data.Settings;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        // void SaveGameData();
        void SaveProgressData();

        void SaveSettingsData();

        // void ClearGameData();
        void ClearProgressData();

        // GameData LoadGameData();
        ProgressData LoadProgressData();
        SettingsData LoadSettingsData();
    }
}