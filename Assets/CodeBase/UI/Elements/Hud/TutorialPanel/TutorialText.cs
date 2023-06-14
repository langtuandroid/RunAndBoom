using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class TutorialText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _shootText;
        [SerializeField] private TextMeshProUGUI _weaponsText;
        [SerializeField] private TextMeshProUGUI _movementText;

        protected override void RuChosen()
        {
            _shootText.text = LocalizationConstants.HudShootRu;
            _weaponsText.text = LocalizationConstants.HudWeaponsRu;
            _movementText.text = LocalizationConstants.HudMovementRu;
        }

        protected override void TrChosen()
        {
            _shootText.text = LocalizationConstants.HudShootTr;
            _weaponsText.text = LocalizationConstants.HudWeaponsTr;
            _movementText.text = LocalizationConstants.HudMovementTr;
        }

        protected override void EnChosen()
        {
            _shootText.text = LocalizationConstants.HudShootEn;
            _weaponsText.text = LocalizationConstants.HudWeaponsEn;
            _movementText.text = LocalizationConstants.HudMovementEn;
        }
    }
}