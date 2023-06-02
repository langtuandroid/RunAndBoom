using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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