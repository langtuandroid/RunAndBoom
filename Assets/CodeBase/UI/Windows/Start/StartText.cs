using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Start
{
    public class StartText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _startButtonText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.StartTitleRu;
            _startButtonText.text = LocalizationConstants.StartButtonRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.StartTitleTr;
            _startButtonText.text = LocalizationConstants.StartButtonTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.StartTitleEn;
            _startButtonText.text = LocalizationConstants.StartButtonEn;
        }
    }
}