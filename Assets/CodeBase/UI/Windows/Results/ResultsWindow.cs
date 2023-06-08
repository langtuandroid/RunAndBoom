using CodeBase.Data;
using CodeBase.Data.Stats;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Gifts;
using Tayx.Graphy.Utils.NumString;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Results
{
    public class ResultsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toGiftsButton;
        [SerializeField] private StarsPanel _starsPanel;
        [SerializeField] private TextMeshProUGUI _playTimeCount;
        [SerializeField] private TextMeshProUGUI _killed;
        [SerializeField] private TextMeshProUGUI _totalEnemies;
        [SerializeField] private TextMeshProUGUI _restartsCount;
        [SerializeField] private TextMeshProUGUI _score;

        private int _maxPrice;
        private Scene _nextScene;

        private void Awake()
        {
            _toGiftsButton.onClick.AddListener(ToGiftsWindow);
            _restartButton.onClick.AddListener(Restart);
        }

        private void ToGiftsWindow()
        {
            Hide();
            WindowBase giftsWindow = WindowService.Show<GiftsWindow>(WindowId.Gifts);
            GiftsGenerator giftsGenerator =
                (giftsWindow as GiftsWindow)?.gameObject.GetComponent<GiftsGenerator>();
            giftsGenerator?.SetMaxPrice(_maxPrice);
            giftsGenerator?.Generate();
            (giftsWindow as GiftsWindow)?.AddNextScene(_nextScene);
        }

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Result);

        public void AddData(Scene nextScene, int maxPrice)
        {
            _nextScene = nextScene;
            _maxPrice = maxPrice;
        }

        public void ShowData()
        {
            Progress.Stats.CurrentLevelStats.CalculateScore();
            // ProgressService.Progress.Stats.CurrentLevelStats.CalculateScore();
            LevelStats levelStats = Progress.Stats.CurrentLevelStats;
            // LevelStats levelStats = ProgressService.Progress.Stats.CurrentLevelStats;
            _starsPanel.ShowStars(levelStats.StarsCount);

            _playTimeCount.text = $"{levelStats.PlayTimeData.PlayTime.ToInt()}";

            _killed.text = $"{levelStats.KillsData.KilledEnemies}";
            _totalEnemies.text = $"{levelStats.KillsData.TotalEnemies}";

            _restartsCount.text = $"{levelStats.RestartsData.Count}";

            _score.text = $"{levelStats.Score}";
        }
    }
}