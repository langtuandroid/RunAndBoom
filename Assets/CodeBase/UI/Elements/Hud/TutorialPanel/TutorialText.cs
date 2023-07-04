using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class TutorialText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _settingsText;
        [SerializeField] private TextMeshProUGUI _aimText;
        [SerializeField] private TextMeshProUGUI _movementText;
        [SerializeField] private TextMeshProUGUI _shootText;
        [SerializeField] private TextMeshProUGUI _weaponsText;

        protected override void RuChosen()
        {
            _settingsText.text = LocalizationConstants.TutorialSettingsRu;
            _aimText.text = LocalizationConstants.TutorialRotationRu;
            _movementText.text = LocalizationConstants.TutorialMovementRu;
            _shootText.text = LocalizationConstants.TutorialShootRu;
            _weaponsText.text = LocalizationConstants.TutorialWeaponsRu;
        }

        protected override void TrChosen()
        {
            _settingsText.text = LocalizationConstants.TutorialSettingsTr;
            _aimText.text = LocalizationConstants.TutorialRotationTr;
            _movementText.text = LocalizationConstants.TutorialMovementTr;
            _shootText.text = LocalizationConstants.TutorialShootTr;
            _weaponsText.text = LocalizationConstants.TutorialWeaponsTr;
        }

        protected override void EnChosen()
        {
            _settingsText.text = LocalizationConstants.TutorialSettingsEn;
            _aimText.text = LocalizationConstants.TutorialRotationEn;
            _movementText.text = LocalizationConstants.TutorialMovementEn;
            _shootText.text = LocalizationConstants.TutorialShootEn;
            _weaponsText.text = LocalizationConstants.TutorialWeaponsEn;
        }
    }
}