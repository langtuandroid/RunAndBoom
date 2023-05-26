using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class HudText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelTitle;

        private PlayerProgress _progress;
        private TextMeshProUGUI _addCounsButtonText;
        private TextMeshProUGUI _nextLevelButtonText;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            // _addCounsButtonText = _addCounsButton.GetComponentInChildren<TextMeshProUGUI>();
            // _nextLevelButtonText = _nextLevelButton.GetComponentInChildren<TextMeshProUGUI>();
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _localizationService.LanguageChanged += ChangeText;
            ChangeText();
        }

        private void ChangeText()
        {
            switch (_localizationService.Language)
                // switch (_progress.SettingsData.Language)
            {
                case Language.RU:
                    _levelTitle.text = LocalizationConstants.LevelRu;
                    break;
                case Language.TR:
                    _levelTitle.text = LocalizationConstants.LevelTr;
                    break;
                default:
                    _levelTitle.text = LocalizationConstants.LevelEn;
                    break;
            }
        }
    }
}