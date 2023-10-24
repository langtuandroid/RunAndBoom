using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPlayerProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        // private const string GameDataKey = "GameData";
        private const string ProgressDataKey = "ProgressData";
        private const string SettingsDataKey = "SettingsData";

        public SaveLoadService(IGameFactory gameFactory)
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _gameFactory = gameFactory;
        }

        public void SaveProgressData()
        {
            foreach (IProgressSaver progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgressData(_progressService.ProgressData);

            PlayerPrefs.SetString(ProgressDataKey, _progressService.ProgressData.ToJson());
            // PlayerPrefs.SetString(ProgressDataKey, _progressService.GameData.ProgressData.ToJson());
        }

        public void SaveSettingsData() =>
            PlayerPrefs.SetString(ProgressDataKey, _progressService.SettingsData.ToJson());

        public void ClearProgressData()
        {
            _progressService.ClearProgressData();
            PlayerPrefs.DeleteAll();
        }

        public ProgressData LoadProgressData()
        {
            string s = PlayerPrefs.GetString(ProgressDataKey);
            return s?.ToDeserialized<ProgressData>();
        }

        public SettingsData LoadSettingsData()
        {
            string s = PlayerPrefs.GetString(SettingsDataKey);
            return s?.ToDeserialized<SettingsData>();
        }

        // public void SaveGameData()
        // {
        //     foreach (IProgressSaver progressWriter in _gameFactory.ProgressWriters)
        //         progressWriter.UpdateProgress(_progressService.ProgressData);
        //
        //     PlayerPrefs.SetString(GameDataKey, _progressService.GameData.ToJson());
        // }

        // public void ClearGameData()
        // {
        //     _progressService.ClearGameData();
        //     PlayerPrefs.DeleteAll();
        // }

        // public GameData LoadGameData()
        // {
        //     string s = PlayerPrefs.GetString(GameDataKey);
        //     return s?.ToDeserialized<GameData>();
        // }
    }
}