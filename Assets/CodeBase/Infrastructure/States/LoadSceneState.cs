using System.Threading.Tasks;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factories;
using CodeBase.Logic.Level;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.StaticData.Levels;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.WeaponsPanel;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Death;
using CodeBase.UI.Windows.GameEnd;
using CodeBase.UI.Windows.Gifts;
using CodeBase.UI.Windows.LeaderBoard;
using CodeBase.UI.Windows.Results;
using CodeBase.UI.Windows.Settings;
using CodeBase.UI.Windows.Shop;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = CodeBase.Data.Scene;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<Scene>
    {
        private const string LevelName = "Level_";
        private const string FinishPointTag = "FinishPoint";

        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;
        private Scene _scene;
        private bool _isInitial = true;
        private GameObject _hud;

        public LoadSceneState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain, IGameFactory gameFactory, IEnemyFactory enemyFactory,
            IPlayerProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory,
            IWindowService windowService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _enemyFactory = enemyFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public void Enter(Scene scene)
        {
            _scene = scene;

            if (_scene.ToString().Contains(LevelName))
            {
                _loadingCurtain.Show();
                _gameFactory.CleanUp();
                _gameFactory.WarmUp();
            }

            _sceneLoader.Load(_scene, OnLoaded);
        }

        public void Exit()
        {
        }

        private async void OnLoaded(Scene scene)
        {
            await InitUIRoot();

            switch (scene)
            {
                case Scene.Level_1:
                    await InitGameWorld();
                    break;
                case Scene.Level_2:
                    await InitGameWorld();
                    break;
                case Scene.Level_3:
                    await InitGameWorld();
                    break;
                case Scene.Level_4:
                    await InitGameWorld();
                    break;
            }

            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private async Task InitGameWorld()
        {
            var levelData = LevelStaticData();

            if (levelData.InitializeHeroPosition)
            {
                _enemyFactory.CreateSpawnersRoot();
                await InitGameWorld(levelData);
                await InitSpawners(levelData);
                _loadingCurtain.Hide();
            }
        }

        private async Task InitUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private LevelStaticData LevelStaticData() =>
            _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

        private async Task InitGameWorld(LevelStaticData levelData)
        {
            GameObject hero = await InitHero(levelData);
            await InitHud(hero);
            await InitWindows(hero);
            InitLevelTransfer(levelData);
        }

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
                await _enemyFactory.CreateSpawner(spawnerData.Position, spawnerData.EnemyTypeId);
        }

        private async Task<GameObject> InitHero(LevelStaticData levelStaticData) =>
            await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

        private void InitLevelTransfer(LevelStaticData levelData)
        {
            GameObject findWithTag = GameObject.FindWithTag(FinishPointTag);
            findWithTag.GetComponent<Finish>().Construct(levelData.LevelTransfer.TransferTo);
        }

        private async Task InitHud(GameObject hero)
        {
            if (_hud == null)
                _hud = await _uiFactory.CreateHud();

            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            heroHealth.Construct(_staticDataService);
            hero.GetComponent<HeroMovement>().Construct(_staticDataService);
            hero.GetComponent<HeroReloading>().Construct(_staticDataService);
            HeroReloading heroReloading = hero.GetComponent<HeroReloading>();
            HeroDeath heroDeath = hero.GetComponent<HeroDeath>();
            HeroWeaponSelection heroWeaponSelection = hero.GetComponentInChildren<HeroWeaponSelection>();
            heroWeaponSelection.Construct(heroDeath, heroReloading);
            _hud.GetComponentInChildren<Health>().Construct(heroHealth);
            _hud.GetComponentInChildren<WeaponsSelecter>().Construct(heroWeaponSelection);
            _hud.GetComponentInChildren<ReloadingIndicator>().Construct(heroReloading, heroWeaponSelection);
            _hud.GetComponentInChildren<Crosshairs>().Construct(heroReloading, heroWeaponSelection);
        }

        private async Task InitWindows(GameObject hero)
        {
            GameObject shopWindow = await _uiFactory.CreateShopWindow();
            shopWindow.GetComponent<ShopWindow>().Construct(hero);
            shopWindow.GetComponent<ShopItemsGenerator>()?.Construct(hero);
            GameObject deathWindow = await _uiFactory.CreateDeathWindow();
            deathWindow.GetComponent<DeathWindow>().Construct(hero);
            GameObject settingsWindow = await _uiFactory.CreateSettingsWindow();
            settingsWindow.GetComponent<SettingsWindow>().Construct(hero);
            GameObject giftsWindow = await _uiFactory.CreateGiftsWindow();
            giftsWindow.GetComponent<GiftsGenerator>()?.Construct(hero);
            giftsWindow.GetComponent<GiftsWindow>()?.Construct(hero);
            GameObject resultsWindow = await _uiFactory.CreateResultsWindow();
            resultsWindow.GetComponent<ResultsWindow>()?.Construct(hero);
            GameObject leaderBoardWindow = await _uiFactory.CreateLeaderBoardWindow();
            leaderBoardWindow.GetComponent<LeaderBoardWindow>()?.Construct(hero);
            GameObject gameEndWindow = await _uiFactory.CreateGameEndWindow();
            gameEndWindow.GetComponent<GameEndWindow>()?.Construct(hero);

            _windowService.AddWindow(WindowId.Shop, shopWindow);
            _windowService.AddWindow(WindowId.Death, deathWindow);
            _windowService.AddWindow(WindowId.Gifts, giftsWindow);
            _windowService.AddWindow(WindowId.Result, resultsWindow);
            _windowService.AddWindow(WindowId.LeaderBoard, leaderBoardWindow);
            _windowService.AddWindow(WindowId.GameEnd, gameEndWindow);
            _windowService.AddWindow(WindowId.Settings, settingsWindow);
        }
    }
}