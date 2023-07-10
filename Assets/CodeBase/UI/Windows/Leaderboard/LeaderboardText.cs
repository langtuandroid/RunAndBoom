using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.LeaderBoard
{
    public class LeaderBoardText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _nextText;

        protected override void RuChosen()
        {
            _restartText.text = LocalizationConstants.RestartRu;
            _nextText.text = LocalizationConstants.NextRu;
        }

        protected override void TrChosen()
        {
            _restartText.text = LocalizationConstants.RestartTr;
            _nextText.text = LocalizationConstants.NextTr;
        }

        protected override void EnChosen()
        {
            _restartText.text = LocalizationConstants.RestartEn;
            _nextText.text = LocalizationConstants.NextEn;
        }
    }
}