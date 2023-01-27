using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.StaticData.Level;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;
        private readonly IAssets _assets;

        private string _sceneName;

        [Inject]
        public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
            IPlayerProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory, IAssets assets)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
            _assets = assets;
        }

        public void Enter(string sceneName)
        {
            _sceneName = sceneName;

            if (_sceneName == Scenes.Level1)
            {
                _loadingCurtain.Show();
            }

            if (_sceneName == Scenes.Level1)
            {
                _gameFactory.CleanUp();
                _gameFactory.WarmUp();
            }

            _sceneLoader.Load(_sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log($"Exit {_sceneName}");
            if (_sceneName == Scenes.Level1)
                _loadingCurtain.Hide();
        }

        private async void OnLoaded(string name)
        {
            // await InitUIRoot();
            // await InitUI(name);

            switch (name)
            {
                case Scenes.Level1:
                    // await InitGameWorld();
                    break;
            }

            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private async Task InitUI(string name)
        {
            // switch (name)
            // {
            //     case Scenes.Main:
            //         await _uiFactory.CreateMainUI();
            //         break;
            //     case Scenes.Map:
            //         await _uiFactory.CreateMapUI();
            //         break;
            //     case Scenes.Armory:
            //         GameObject armory = await _uiFactory.CreateArmoryUI();
            //         WeaponsSelection weaponsSelection = armory.GetComponentInChildren<WeaponsSelection>();
            //         // weaponsSelection.Construct(_progressService, _assets, _staticData, _uiFactory);
            //         weaponsSelection.Initialize();
            //         break;
            //     case Scenes.Settings:
            //         await _uiFactory.CreateSettingsUI();
            //         break;
            //     case Scenes.Level1:
            //         break;
            // }
        }

        private async Task InitGameWorld()
        {
            var levelData = LevelStaticData();

            await InitSpawners(levelData);

            if (levelData.InitializeHeroPosition)
            {
                GameObject hero = await InitHero(levelData);
                // await InitHud(hero);
                // CameraFollow(hero);
            }
        }

        private async Task<GameObject> InitHero(LevelStaticData levelStaticData) =>
            await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
                await _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.MonsterTypeId);
        }

        private async Task InitHud(GameObject player)
        {
            GameObject hud = await _uiFactory.CreateHud();
            HeroHealth heroHealth = player.GetComponent<HeroHealth>();
            heroHealth.Construct();
            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }

        private LevelStaticData LevelStaticData() =>
            _staticData.ForLevel(SceneManager.GetActiveScene().name);

        private void CameraFollow(GameObject player) =>
            Camera.main.GetComponent<CameraFollower>().Follow(player);

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadSceneState>
        {
        }
    }
}