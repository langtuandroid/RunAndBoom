using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class TutorialText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _settingsText;
        [SerializeField] private TextMeshProUGUI _movementText;
        [SerializeField] private TextMeshProUGUI _shootText;
        [SerializeField] private TextMeshProUGUI _weaponsText;

        protected override void RuChosen()
        {
            _settingsText.text = LocalizationConstants.HudSettingsRu;
            _movementText.text = LocalizationConstants.HudMovementRu;
            _shootText.text = LocalizationConstants.HudShootRu;
            _weaponsText.text = LocalizationConstants.HudWeaponsRu;
        }

        protected override void TrChosen()
        {
            _settingsText.text = LocalizationConstants.HudSettingsTr;
            _movementText.text = LocalizationConstants.HudMovementTr;
            _shootText.text = LocalizationConstants.HudShootTr;
            _weaponsText.text = LocalizationConstants.HudWeaponsTr;
        }

        protected override void EnChosen()
        {
            _settingsText.text = LocalizationConstants.HudSettingsEn;
            _movementText.text = LocalizationConstants.HudMovementEn;
            _shootText.text = LocalizationConstants.HudShootEn;
            _weaponsText.text = LocalizationConstants.HudWeaponsEn;
        }
    }
}