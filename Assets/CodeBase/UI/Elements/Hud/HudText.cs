using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class HudText : BaseText
    {
        [SerializeField] private TextMeshProUGUI _levelTitle;
        [SerializeField] private TextMeshProUGUI _grenadeLauncherText;
        [SerializeField] private TextMeshProUGUI _rpgText;
        [SerializeField] private TextMeshProUGUI _rocketLauncherText;
        [SerializeField] private TextMeshProUGUI _mortarText;

        protected override void RuChosen()
        {
            _levelTitle.text = LocalizationConstants.HudLevelRu;
            _grenadeLauncherText.text = LocalizationConstants.HudGrenadeLauncherRu;
            _rpgText.text = LocalizationConstants.HudRpgRu;
            _rocketLauncherText.text = LocalizationConstants.HudRocketLauncherRu;
            _mortarText.text = LocalizationConstants.HudMortarRu;
        }

        protected override void TrChosen()
        {
            _levelTitle.text = LocalizationConstants.HudLevelTr;
            _grenadeLauncherText.text = LocalizationConstants.HudGrenadeLauncherTr;
            _rpgText.text = LocalizationConstants.HudRpgTr;
            _rocketLauncherText.text = LocalizationConstants.HudRocketLauncherTr;
            _mortarText.text = LocalizationConstants.HudMortarTr;
        }

        protected override void EnChosen()
        {
            _levelTitle.text = LocalizationConstants.HudLevelEn;
            _grenadeLauncherText.text = LocalizationConstants.HudGrenadeLauncherEn;
            _rpgText.text = LocalizationConstants.HudRpgEn;
            _rocketLauncherText.text = LocalizationConstants.HudRocketLauncherEn;
            _mortarText.text = LocalizationConstants.HudMortarEn;
        }
    }
}