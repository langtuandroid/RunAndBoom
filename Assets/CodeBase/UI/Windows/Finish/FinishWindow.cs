using CodeBase.Services;
using CodeBase.Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private ISaveLoadService _saveLoadService;

        protected override void OnAwake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _saveLoadService.SaveProgress();
            base.OnAwake();
        }

        protected override void Initialize() =>
            RefreshScoreText();

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
    }
}