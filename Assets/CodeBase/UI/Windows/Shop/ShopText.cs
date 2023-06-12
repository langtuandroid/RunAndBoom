using CodeBase.Services.Localization;
using CodeBase.UI.Elements;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopText : BaseText
    {
        protected override void RuChosen() =>
            Title.text = LocalizationConstants.ShopTitleRu;

        protected override void TrChosen() =>
            Title.text = LocalizationConstants.ShopTitleTr;

        protected override void EnChosen() =>
            Title.text = LocalizationConstants.ShopTitleEn;
    }
}