using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _nextText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.FinishTitleRu;
            _nextText.text = LocalizationConstants.NextRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.FinishTitleTr;
            _nextText.text = LocalizationConstants.NextTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.FinishTitleEn;
            _nextText.text = LocalizationConstants.NextEn;
        }
    }
}