using CodeBase.Services.Localization;
using CodeBase.UI.Elements;

namespace CodeBase.UI.Windows.Death
{
    public class DeathText : BaseText
    {
        protected override void RuChosen() =>
            Title.text = LocalizationConstants.DeathTitleRu;

        protected override void TrChosen() =>
            Title.text = LocalizationConstants.DeathTitleTr;

        protected override void EnChosen() =>
            Title.text = LocalizationConstants.DeathTitleEn;
    }
}