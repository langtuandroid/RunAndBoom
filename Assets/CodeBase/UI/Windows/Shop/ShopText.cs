using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _refreshText;
        [SerializeField] private TextMeshProUGUI _skipText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.ShopTitleRu;
            _refreshText.text = LocalizationConstants.ShopRefreshRu;
            _skipText.text = LocalizationConstants.ShopSkipRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.ShopTitleTr;
            _refreshText.text = LocalizationConstants.ShopRefreshTr;
            _skipText.text = LocalizationConstants.ShopSkipTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.ShopTitleEn;
            _refreshText.text = LocalizationConstants.ShopRefreshEn;
            _skipText.text = LocalizationConstants.ShopSkipEn;
        }
    }
}