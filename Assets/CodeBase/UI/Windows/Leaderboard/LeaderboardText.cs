using CodeBase.Services.Localization;
using CodeBase.UI.Windows.Common;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.LeaderBoard
{
    public class LeaderBoardText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _next;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.LeaderBoardTitleRu;
            _next.text = LocalizationConstants.NextRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.LeaderBoardTitleTr;
            _next.text = LocalizationConstants.NextTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.LeaderBoardTitleEn;
            _next.text = LocalizationConstants.NextEn;
        }
    }
}