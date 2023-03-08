using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPlayerProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        private const string ProgressKey = "Progress";

        public SaveLoadService(IPlayerProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (IProgressSaver progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}