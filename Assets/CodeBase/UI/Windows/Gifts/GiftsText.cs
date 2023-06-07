using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;

        protected override void RuChosen() =>
            _title.text = LocalizationConstants.FinishTitleRu;

        protected override void TrChosen() =>
            _title.text = LocalizationConstants.FinishTitleTr;

        protected override void EnChosen() =>
            _title.text = LocalizationConstants.FinishTitleEn;
    }
}