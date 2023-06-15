using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Death
{
    public class DeathText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _recoverText;
        [SerializeField] private TextMeshProUGUI _restartText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.DeathTitleRu;
            _recoverText.text = LocalizationConstants.DeathRecoverRu;
            _restartText.text = LocalizationConstants.RestartRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.DeathTitleTr;
            _recoverText.text = LocalizationConstants.DeathRecoverTr;
            _restartText.text = LocalizationConstants.RestartTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.DeathTitleEn;
            _recoverText.text = LocalizationConstants.DeathRecoverEn;
            _restartText.text = LocalizationConstants.RestartEn;
        }
    }
}