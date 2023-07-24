using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Elements.Hud.LeaderBoardButton
{
    public class LeaderBoardButtonText : BaseText
    {
        protected override void RuChosen() =>
            Title.text = LocalizationConstants.LeaderBoardTitleRu;

        protected override void TrChosen() =>
            Title.text = LocalizationConstants.LeaderBoardTitleTr;

        protected override void EnChosen() =>
            Title.text = LocalizationConstants.LeaderBoardTitleEn;
    }
}