using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class ScoreCounter : MonoBehaviour, IProgressSaver
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        private LevelStats _currentLevelStats;

        private void ScoreChanged() =>
            _counterText.text = _currentLevelStats.ScoreData.Score.ToString();

        public void LoadProgress(PlayerProgress progress)
        {
            _currentLevelStats = progress.CurrentLevelStats;
            _counterText.text = _currentLevelStats.ScoreData.Score.ToString();
            _currentLevelStats.ScoreData.ScoreChanged += ScoreChanged;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}