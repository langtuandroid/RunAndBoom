using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishWindow : WindowBase
    {
        [SerializeField] private Button _addCoinsButton;
        [SerializeField] private Button _toNextLevelButton;
        [SerializeField] private ItemsGeneratorBase _generator;

        private ISaveLoadService _saveLoadService;
        private IGameStateMachine _gameStateMachine;
        private IPlayerProgressService _playerProgressService;
        private Scene _scene;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _addCoinsButton.onClick.AddListener(ShowAds);
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
            _generator.GenerationStarted += DisableRefreshButtons;
            _generator.GenerationEnded += CheckRefreshButtons;
        }

        private void OnEnable()
        {
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
        }

        private void OnDisable()
        {
            _toNextLevelButton.onClick.RemoveListener(ToNextLevel);
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Finish);

        public void AddScene(Scene scene)
        {
            _scene = scene;
            GenerateItems();
        }

        private void DisableRefreshButtons()
        {
        }

        private void CheckRefreshButtons()
        {
        }

        private void Start() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void ToNextLevel()
        {
            _saveLoadService.SaveProgress();
            _playerProgressService.Progress.StartNewLevel(_scene);
            _playerProgressService.Progress.WorldData.LevelNameData.ChangeLevel(_scene.ToString());
            _gameStateMachine.Enter<LoadSceneState, Scene>(_scene);
            CloseWindow();
        }

        private void CloseWindow()
        {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ShowAds()
        {
            //TODO ShowAds screen
        }

        private void GenerateItems() =>
            _generator.Generate();

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.VictoryMusic), transform: transform,
                Volume, AudioSource);
        }
    }
}