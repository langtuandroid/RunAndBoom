using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _restartText;

        protected override void RuChosen()
        {
            Title.text = LocalizationConstants.SettingsTitleRu;
            _restartText.text = LocalizationConstants.RestartRu;
        }

        protected override void TrChosen()
        {
            Title.text = LocalizationConstants.SettingsTitleTr;
            _restartText.text = LocalizationConstants.RestartTr;
        }

        protected override void EnChosen()
        {
            Title.text = LocalizationConstants.SettingsTitleEn;
            _restartText.text = LocalizationConstants.RestartEn;
        }
    }
}