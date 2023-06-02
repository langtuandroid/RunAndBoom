using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;

        protected override void RuChosen() =>
            _title.text = LocalizationConstants.ShopTitleRu;

        protected override void TrChosen() =>
            _title.text = LocalizationConstants.ShopTitleTr;

        protected override void EnChosen() =>
            _title.text = LocalizationConstants.ShopTitleEn;
    }
}