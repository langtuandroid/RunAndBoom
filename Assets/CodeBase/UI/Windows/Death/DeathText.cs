using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathText : MonoBehaviour
        //, IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _restartButton;

        // private PlayerProgress _progress;
        private TextMeshProUGUI _restartButtonText;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            _restartButtonText = _restartButton.GetComponentInChildren<TextMeshProUGUI>();
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
                    _title.text = LocalizationConstants.DeathTitleRu;
                    break;
                case Language.TR:
                    _restartButtonText.text = LocalizationConstants.RestartTr;
                    _title.text = LocalizationConstants.DeathTitleTr;
                    break;
                default:
                    _restartButtonText.text = LocalizationConstants.RestartEn;
                    _title.text = LocalizationConstants.DeathTitleEn;
                    break;
            }
        }
    }
}