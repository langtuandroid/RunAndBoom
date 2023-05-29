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

        public SaveLoadService(IGameFactory gameFactory)
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (IProgressSaver progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            string s = PlayerPrefs.GetString(ProgressKey);
            return s?.ToDeserialized<PlayerProgress>();
        }
    }
}