using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class ScoreUI : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _score;

        private string _sector;
        private ScoreData _scoreData;

        public void LoadProgress(PlayerProgress progress)
        {
            _scoreData = progress.CurrentLevelStats.ScoreData;
            _scoreData.ScoreChanged += SetScore;
            SetScore();
        }

        private void SetScore() =>
            _score.text = $"{_scoreData.Score}";
    }
}