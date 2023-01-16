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
using CodeBase.UI.Screens.Armory;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        [Inject] private readonly IGameStateMachine _stateMachine;
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly ILoadingCurtain _loadingCurtain;
        [Inject] private readonly IGameFactory _gameFactory;
        [Inject] private readonly IPlayerProgressService _progressService;
        [Inject] private readonly IStaticDataService _staticData;
        [Inject] private readonly IUIFactory _uiFactory;
        [Inject] private readonly IAssets _assets;

        private string _sceneName;

        // public LoadSceneState(GameStateMachine gameStateMachine
        //     // , SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
        //     // IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData,
        //     // IUIFactory uiFactory, IAssets assets
        //     )
        // {
        //     _stateMachine = gameStateMachine;
        //     // _sceneLoader = sceneLoader;
        //     // _loadingCurtain = loadingCurtain;
        //     // _gameFactory = gameFactory;
        //     // _progressService = progressService;
        //     // _staticData = staticData;
        //     // _uiFactory = uiFactory;
        //     // _assets = assets;
        // }

        public void Enter(string sceneName)
        {
            _sceneName = sceneName;

            if (_sceneName == Scenes.Level1 || _sceneName == Scenes.Initial)
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
            if (_sceneName == Scenes.Level1 || _sceneName == Scenes.Initial)
                _loadingCurtain.Hide();
        }

        private async void OnLoaded(string name)
        {
            await InitUIRoot();
            await InitUI(name);

            switch (name)
            {
                case Scenes.Level1:
                    await InitGameWorld();
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
            switch (name)
            {
                case Scenes.Main:
                    await _uiFactory.CreateMainUI();
                    break;
                case Scenes.Map:
                    await _uiFactory.CreateMapUI();
                    break;
                case Scenes.Armory:
                    GameObject armory = await _uiFactory.CreateArmoryUI();
                    WeaponsSelection weaponsSelection = armory.GetComponentInChildren<WeaponsSelection>();
                    // weaponsSelection.Construct(_progressService, _assets, _staticData, _uiFactory);
                    weaponsSelection.Initialize();
                    break;
                case Scenes.Settings:
                    await _uiFactory.CreateSettingsUI();
                    break;
                case Scenes.Level1:
                    break;
            }
        }

        private async Task InitGameWorld()
        {
            var levelData = LevelStaticData();

            await InitSpawners(levelData);

            if (levelData.InitializePersonPosition)
            {
                GameObject player = await InitPlayer(levelData);
                await InitHud(player);
                CameraFollow(player);
            }
        }

        private async Task<GameObject> InitPlayer(LevelStaticData levelStaticData) =>
            await _gameFactory.CreatePlayer(levelStaticData.InitialPersonPosition);

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