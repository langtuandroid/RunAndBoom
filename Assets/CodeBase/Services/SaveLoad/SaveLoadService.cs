using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        [Inject] private readonly IPlayerProgressService _progressService;
        [Inject] private readonly IGameFactory _gameFactory;

        private const string ProgressKey = "Progress";

        // public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        // {
        //     _progressService = progressService;
        //     _gameFactory = gameFactory;
        // }

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