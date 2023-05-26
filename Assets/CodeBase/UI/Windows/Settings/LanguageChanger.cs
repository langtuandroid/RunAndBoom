using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class LanguageChanger : MonoBehaviour
        // , IProgressSaver
    {
        [SerializeField] private Button _ruButton;
        [SerializeField] private Button _trButton;
        [SerializeField] private Button _enButton;
        [SerializeField] private Image _ruSelection;
        [SerializeField] private Image _trSelection;
        [SerializeField] private Image _enSelection;

        private PlayerProgress _progress;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _ruButton.onClick.AddListener(RuClicked);
            _trButton.onClick.AddListener(TrClicked);
            _enButton.onClick.AddListener(EnClicked);
            _localizationService.LanguageChanged += ChangeText;
            ChangeText();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            // _progress.SettingsData.LanguageChanged += ChangeText;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            _progress.SettingsData.SetLanguage(_localizationService.Language);
        }

        private void RuClicked()
        {
            // _ruButton.
            _localizationService.ChangeLanguage(Language.RU);
        }

        private void TrClicked()
        {
            _localizationService.ChangeLanguage(Language.TR);
        }

        private void EnClicked()
        {
            _localizationService.ChangeLanguage(Language.EN);
        }

        private void ChangeText()
        {
            switch (_localizationService.Language)
                // switch (_progress.SettingsData.Language)
            {
                case Language.RU:
                    _ruSelection.ChangeImageAlpha(Constants.AlphaActiveItem);
                    _trSelection.ChangeImageAlpha(Constants.AlphaInactiveItem);
                    _enSelection.ChangeImageAlpha(Constants.AlphaInactiveItem);
                    break;
                case Language.TR:
                    _trSelection.ChangeImageAlpha(Constants.AlphaActiveItem);
                    _ruSelection.ChangeImageAlpha(Constants.AlphaInactiveItem);
                    _enSelection.ChangeImageAlpha(Constants.AlphaInactiveItem);
                    break;
                default:
                    _enSelection.ChangeImageAlpha(Constants.AlphaActiveItem);
                    _ruSelection.ChangeImageAlpha(Constants.AlphaInactiveItem);
                    _trSelection.ChangeImageAlpha(Constants.AlphaInactiveItem);
                    break;
            }
        }
    }
}