using CodeBase.Data.Progress;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.MobileInputPanel;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using CodeBase.UI.Windows.Gifts;
using CodeBase.UI.Windows.LeaderBoard;
using Tayx.Graphy.Utils.NumString;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Results
{
    public class ResultsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toNextWindowButton;
        [SerializeField] private StarsPanel _starsPanel;

        [SerializeField] private TextMeshProUGUI _playTimeCount;

        // [SerializeField] private TextMeshProUGUI _killed;
        // [SerializeField] private TextMeshProUGUI _totalEnemies;
        [SerializeField] private TextMeshProUGUI _restartsCount;
        [SerializeField] private TextMeshProUGUI _score;

        private SceneId _nextSceneId;
        private int _maxPrice;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartLevel);

            if (Application.isEditor || LeaderBoardService == null || ProgressData == null)
                return;

            LeaderBoardService.OnInitializeSuccess += RequestLeaderBoard;
            InitializeLeaderBoard();
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartLevel);

            if (LeaderBoardService != null)
                LeaderBoardService.OnInitializeSuccess -= RequestLeaderBoard;
        }

        public void Construct(GameObject hero, OpenSettings openSettings, MobileInput mobileInput) =>
            base.Construct(hero, WindowId.Result, openSettings, mobileInput);

        public void AddData(SceneId currentLevel, SceneId nextLevel, int maxPrice)
        {
            CurrentLevel = currentLevel;
            _nextSceneId = nextLevel;
            _maxPrice = maxPrice;

            if (_nextSceneId == SceneId.Initial)
                _toNextWindowButton.onClick.AddListener(ToGameLeaderBoardWindow);
            else
                _toNextWindowButton.onClick.AddListener(ToGiftsWindow);
        }

        public void ShowData()
        {
            Debug.Log($"ShowData LevelStats.StarsCount {LevelStats.StarsCount}");
            _starsPanel.ShowStars(LevelStats.StarsCount);
            _playTimeCount.text = $"{LevelStats.PlayTimeData.PlayTime.ToInt()}";
            // _killed.text = $"{LevelStats.KillsData.KilledEnemies}";
            // _totalEnemies.text = $"{LevelStats.KillsData.TotalEnemies}";
            _restartsCount.text = $"{LevelStats.RestartsData.Count}";
            Debug.Log($"ShowData {LevelStats.SceneId} {LevelStats.Score}");
            _score.text = $"{LevelStats.Score}";
        }

        public void CalculateScore() =>
            LevelStats.CalculateScore();

        private void ToGameLeaderBoardWindow()
        {
            WindowBase windowBase = WindowService.Show<LeaderBoardWindow>(WindowId.LeaderBoard);
            (windowBase as LeaderBoardWindow)?.SetGameLeaderBoard();
        }

        private void ToGiftsWindow()
        {
            WindowBase giftsWindow = WindowService.Show<GiftsWindow>(WindowId.Gifts);
            (giftsWindow as GiftsWindow).AddData(_nextSceneId);
            GiftsGenerator giftsGenerator = (giftsWindow as GiftsWindow)?.gameObject.GetComponent<GiftsGenerator>();
            giftsGenerator?.SetMaxPrice(_maxPrice);
            giftsGenerator?.Generate();
        }

        protected override void RequestLeaderBoard()
        {
            base.RequestLeaderBoard();
            AddLevelResult();
        }
    }
}