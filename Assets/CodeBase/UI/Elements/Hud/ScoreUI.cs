using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class ScoreUI : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _score;

        private string _sector;
        private MoneyData _moneyData;

        public void LoadProgressData(ProgressData progressData)
        {
            _moneyData = progressData.AllStats.AllMoney;
            _moneyData.MoneyChanged += SetMoney;
            SetMoney();
        }

        private void SetMoney() =>
            _score.text = $"{_moneyData.Money}";
    }
}