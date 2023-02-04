using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private ISaveLoadService _saveLoadService;

        protected override void OnAwake()
        {
            _saveLoadService.SaveProgress();
            base.OnAwake();
        }

        [Inject]
        public void Construct(IPlayerProgressService progressService, ISaveLoadService saveLoadService)
        {
            base.Construct(progressService);
            _saveLoadService = saveLoadService;
        }

        protected override void Initialize()
        {
            RefreshScoreText();
        }

        private void RefreshScoreText() =>
            _scoreText.text = Progress.CurrentLevelStats.ScoreData.Score.ToString();

        protected override void SubscribeUpdates()
        {
            Progress.CurrentLevelStats.ScoreData.ScoreChanged += RefreshScoreText;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            Progress.CurrentLevelStats.ScoreData.ScoreChanged -= RefreshScoreText;
        }

        public class Factory : PlaceholderFactory<IWindowService, FinishWindow>
        {
        }
    }
}