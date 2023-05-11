using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Pool;
using CodeBase.Services.Registrator;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private const float Yaddition = 0.5f;
        private readonly IAssets _assets;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IRegistratorService _registratorService;
        private IObjectsPoolService _objectsPoolService;
        private IGameStateMachine _gameStateMachine;
        private GameObject _heroGameObject;

        public List<IProgressReader> ProgressReaders { get; set; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; set; } = new List<IProgressSaver>();

        public GameFactory(IAssets assets, IObjectsPoolService objectsPoolService,
            IRegistratorService registratorService, IGameStateMachine gameStateMachine)
        {
            _objectsPoolService = objectsPoolService;
            _assets = assets;
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _staticData = AllServices.Container.Single<IStaticDataService>();
            _registratorService = registratorService;
            _gameStateMachine = gameStateMachine;

            SetProgressReadersWriters(registratorService);
        }

        public GameObject GetHero() =>
            _heroGameObject;

        private void SetProgressReadersWriters(IRegistratorService registratorService)
        {
            ProgressReaders = registratorService.ProgressReaders;
            ProgressWriters = registratorService.ProgressWriters;
        }

        public async Task WarmUp()
        {
            _assets.Initialize();
            _objectsPoolService.GenerateObjects();
        }

        public async Task<GameObject> CreateHero(Vector3 at)
        {
            _heroGameObject =
                await _registratorService.InstantiateRegisteredAsync(AssetAddresses.Hero, at.AddY(Yaddition));

            return _heroGameObject;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.CleanUp();
        }
    }
}