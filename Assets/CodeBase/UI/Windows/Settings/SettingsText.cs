using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsText : MonoBehaviour
        // , IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;

        private PlayerProgress _progress;
        private TextMeshProUGUI _restartButtonText;
        private TextMeshProUGUI _closeButtonText;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            _restartButtonText = _restartButton.GetComponentInChildren<TextMeshProUGUI>();
            _closeButtonText = _closeButton.GetComponentInChildren<TextMeshProUGUI>();
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _localizationService.LanguageChanged += ChangeText;
            ChangeText();
        }

        // public void LoadProgress(PlayerProgress progress)
        // {
        //     _progress = progress;
        //     _progress.SettingsData.LanguageChanged += ChangeText;
        // }

        private void ChangeText()
        {
            switch (_localizationService.Language)
                // switch (_progress.SettingsData.Language)
            {
                case Language.RU:
                    _restartButtonText.text = LocalizationConstants.RestartRu;
                    _closeButtonText.text = LocalizationConstants.CloseRu;
                    _title.text = LocalizationConstants.SettingsTitleRu;
                    break;
                case Language.TR:
                    _restartButtonText.text = LocalizationConstants.RestartTr;
                    _closeButtonText.text = LocalizationConstants.CloseTr;
                    _title.text = LocalizationConstants.SettingsTitleTr;
                    break;
                default:
                    _restartButtonText.text = LocalizationConstants.RestartEn;
                    _closeButtonText.text = LocalizationConstants.CloseEn;
                    _title.text = LocalizationConstants.SettingsTitleEn;
                    break;
            }
        }
    }
}