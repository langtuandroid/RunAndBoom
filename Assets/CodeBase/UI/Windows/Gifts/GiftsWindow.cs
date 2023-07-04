using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData.Levels;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsWindow : WindowBase
    {
        [SerializeField] private Button _addCoinsButton;
        [SerializeField] private Button _toNextLevelButton;
        [SerializeField] private ItemsGeneratorBase _generator;

        private Scene _nextScene;

        private void Awake()
        {
            _generator.GenerationStarted += DisableRefreshButtons;
            _generator.GenerationEnded += CheckRefreshButtons;
        }

        private void OnEnable()
        {
            _addCoinsButton.onClick.AddListener(ShowAds);
            _toNextLevelButton.onClick.AddListener(ToNextLevel);
            _generator.GenerationStarted += DisableRefreshButtons;
            _generator.GenerationEnded += CheckRefreshButtons;
        }

        private void OnDisable()
        {
            _addCoinsButton.onClick.RemoveListener(ShowAds);
            _toNextLevelButton.onClick.RemoveListener(ToNextLevel);
            _generator.GenerationStarted -= DisableRefreshButtons;
            _generator.GenerationEnded -= CheckRefreshButtons;
        }

        public void Construct(GameObject hero)
        {
            base.Construct(hero, WindowId.Gifts);
        }

        public void AddNextScene(Scene nextScene)
        {
            _nextScene = nextScene;
            GenerateItems();
        }

        private void DisableRefreshButtons()
        {
        }

        private void CheckRefreshButtons()
        {
        }

        private void Start()
        {
            if (!Application.isMobilePlatform)
                Cursor.lockState = CursorLockMode.Confined;
        }

        private void ToNextLevel()
        {
            LevelStaticData levelStaticData = StaticDataService.ForLevel(_nextScene.ToString());
            WindowService.HideAll();
            Progress.Stats.StartNewLevel(_nextScene, levelStaticData.TargetPlayTime,
                levelStaticData.EnemySpawners.Count);
            Progress.WorldData.LevelNameData.ChangeLevel(_nextScene.ToString());
            SaveLoadService.SaveProgress();
            GameStateMachine.Enter<LoadSceneState, Scene>(_nextScene);
            Close();
        }

        private void Close() =>
            Hide();

        private void ShowAds()
        {
            //TODO ShowAds screen
        }

        private void GenerateItems()
        {
            _generator.Generate();
        }

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.VictoryMusic), transform: transform,
                Volume, AudioSource);
            SoundInstance.StopRandomMusic(false);
        }
    }
}