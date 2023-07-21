using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.GameEnd
{
    public class GameEndText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _writeReviewText;
        [SerializeField] private TextMeshProUGUI _startNewStandardGameText;
        [SerializeField] private TextMeshProUGUI _startNewHardGameText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.GameEndTitleRu;
            _writeReviewText.text = LocalizationConstants.GameEndWriteReviewRu;
            _startNewStandardGameText.text = LocalizationConstants.GameEndStartNewStardardGameRu;
            _startNewHardGameText.text = LocalizationConstants.GameEndStartNewHardGameRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.GameEndTitleTr;
            _writeReviewText.text = LocalizationConstants.GameEndWriteReviewTr;
            _startNewStandardGameText.text = LocalizationConstants.GameEndStartNewStardardGameTr;
            _startNewHardGameText.text = LocalizationConstants.GameEndStartNewHardGameTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.GameEndTitleEn;
            _writeReviewText.text = LocalizationConstants.GameEndWriteReviewEn;
            _startNewStandardGameText.text = LocalizationConstants.GameEndStartNewStardardGameEn;
            _startNewHardGameText.text = LocalizationConstants.GameEndStartNewHardGameEn;
        }
    }
}