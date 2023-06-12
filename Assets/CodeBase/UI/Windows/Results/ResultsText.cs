using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Results
{
    public class ResultsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _playTimeSec;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.ResultsTitleRu;
            _playTimeSec.text = LocalizationConstants.ResultsSecRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.ResultsTitleTr;
            _playTimeSec.text = LocalizationConstants.ResultsSecTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.ResultsTitleEn;
            _playTimeSec.text = LocalizationConstants.ResultsSecEn;
        }
    }
}