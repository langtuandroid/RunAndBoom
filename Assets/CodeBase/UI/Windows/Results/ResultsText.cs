using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Results
{
    public class ResultsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _playTimeSec;

        protected override void RuChosen()
        {
            _title.text = LocalizationConstants.ResultsTitleRu;
            _playTimeSec.text = LocalizationConstants.ResultsSecRu;
        }

        protected override void TrChosen()
        {
            _title.text = LocalizationConstants.ResultsTitleTr;
            _playTimeSec.text = LocalizationConstants.ResultsSecTr;
        }

        protected override void EnChosen()
        {
            _title.text = LocalizationConstants.ResultsTitleEn;
            _playTimeSec.text = LocalizationConstants.ResultsSecEn;
        }
    }
}