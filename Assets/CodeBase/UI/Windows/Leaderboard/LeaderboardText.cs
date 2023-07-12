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
            Title.text = LocalizationConstants.LeaderBoardTitleRu;
            _restartText.text = LocalizationConstants.RestartRu;
            _nextText.text = LocalizationConstants.NextRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.LeaderBoardTitleTr;
            _restartText.text = LocalizationConstants.RestartTr;
            _nextText.text = LocalizationConstants.NextTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.LeaderBoardTitleEn;
            _restartText.text = LocalizationConstants.RestartEn;
            _nextText.text = LocalizationConstants.NextEn;
        }
    }
}