using CodeBase.Services.Localization;
using CodeBase.UI.Elements;

namespace CodeBase.UI.Windows.Gifts
{
    public class GiftsText : BaseText
    {
        protected override void RuChosen() =>
            Title.text = LocalizationConstants.FinishTitleRu;

        protected override void TrChosen() =>
            Title.text = LocalizationConstants.FinishTitleTr;

        protected override void EnChosen() =>
            Title.text = LocalizationConstants.FinishTitleEn;
    }
}