using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Results
{
    public class ResultsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _playTimeSecText;
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _nextText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.ResultsTitleRu;
            _playTimeSecText.text = LocalizationConstants.ResultsSecRu;
            _restartText.text = LocalizationConstants.RestartRu;
            _nextText.text = LocalizationConstants.NextRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.ResultsTitleTr;
            _playTimeSecText.text = LocalizationConstants.ResultsSecTr;
            _restartText.text = LocalizationConstants.RestartTr;
            _nextText.text = LocalizationConstants.NextTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.ResultsTitleEn;
            _playTimeSecText.text = LocalizationConstants.ResultsSecEn;
            _restartText.text = LocalizationConstants.RestartEn;
            _nextText.text = LocalizationConstants.NextEn;
        }
    }
}