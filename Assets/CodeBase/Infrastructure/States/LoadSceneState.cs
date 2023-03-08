using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.StaticData.Level;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;

        private string _sceneName;

        public LoadSceneState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, IGameFactory gameFactory,
            IEnemyFactory enemyFactory, IPlayerProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _enemyFactory = enemyFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
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
            if (_sceneName == Scenes.Level1)
                _loadingCurtain.Hide();
        }

        private async void OnLoaded(string name)
        {
            await InitUIRoot();

            switch (name)
            {
                case Scenes.Level1:
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
                await InitGameWorld(levelData);
                await InitSpawners(levelData);
            }
        }

        private async Task InitUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private LevelStaticData LevelStaticData() =>
            _staticData.ForLevel(SceneManager.GetActiveScene().name);

        private async Task InitGameWorld(LevelStaticData levelData)
        {
            GameObject hero = await InitHero(levelData);
            await InitHud(hero);
        }

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
                await _enemyFactory.CreateSpawner(spawnerData.Position, spawnerData.EnemyTypeId);
        }

        private async Task<GameObject> InitHero(LevelStaticData levelStaticData) =>
            await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

        private async Task InitHud(GameObject hero)
        {
            GameObject hud = await _uiFactory.CreateHud();
            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            heroHealth.Construct();
            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
        }
    }
}