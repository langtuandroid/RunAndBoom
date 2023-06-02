using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _launchTrainingText;

        protected override void RuChosen()
        {
            _title.text = LocalizationConstants.SettingsTitleRu;
            _launchTrainingText.text = LocalizationConstants.SettingsLaunchTrainingRu;
        }

        protected override void TrChosen()
        {
            _title.text = LocalizationConstants.SettingsTitleTr;
            _launchTrainingText.text = LocalizationConstants.SettingsLaunchTrainingTr;
        }

        protected override void EnChosen()
        {
            _title.text = LocalizationConstants.SettingsTitleEn;
            _launchTrainingText.text = LocalizationConstants.SettingsLaunchTrainingEn;
        }
    }
}