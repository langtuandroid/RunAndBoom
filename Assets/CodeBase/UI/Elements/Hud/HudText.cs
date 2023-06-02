using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
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
            _levelTitle.text = LocalizationConstants.LevelRu;
            _grenadeLauncherText.text = LocalizationConstants.GrenadeLauncherRu;
            _rpgText.text = LocalizationConstants.RpgRu;
            _rocketLauncherText.text = LocalizationConstants.RocketLauncherRu;
            _mortarText.text = LocalizationConstants.MortarRu;
        }

        protected override void TrChosen()
        {
            _levelTitle.text = LocalizationConstants.LevelTr;
            _grenadeLauncherText.text = LocalizationConstants.GrenadeLauncherTr;
            _rpgText.text = LocalizationConstants.RpgTr;
            _rocketLauncherText.text = LocalizationConstants.RocketLauncherTr;
            _mortarText.text = LocalizationConstants.MortarTr;
        }

        protected override void EnChosen()
        {
            _levelTitle.text = LocalizationConstants.LevelEn;
            _grenadeLauncherText.text = LocalizationConstants.GrenadeLauncherEn;
            _rpgText.text = LocalizationConstants.RpgEn;
            _rocketLauncherText.text = LocalizationConstants.RocketLauncherEn;
            _mortarText.text = LocalizationConstants.MortarEn;
        }
    }
}