using CodeBase.Services.Localization;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _title;

        protected override void RuChosen() =>
            _title.text = LocalizationConstants.SettingsTitleRu;

        protected override void TrChosen() =>
            _title.text = LocalizationConstants.SettingsTitleTr;

        protected override void EnChosen() =>
            _title.text = LocalizationConstants.SettingsTitleEn;
    }
}