using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishText : MonoBehaviour
        // , IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _addCounsButton;
        [SerializeField] private Button _nextLevelButton;

        private PlayerProgress _progress;
        private TextMeshProUGUI _addCounsButtonText;
        private TextMeshProUGUI _nextLevelButtonText;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            _addCounsButtonText = _addCounsButton.GetComponentInChildren<TextMeshProUGUI>();
            _nextLevelButtonText = _nextLevelButton.GetComponentInChildren<TextMeshProUGUI>();
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
                    _addCounsButtonText.text = LocalizationConstants.AddCoinsRu;
                    _nextLevelButtonText.text = LocalizationConstants.NextLevelRu;
                    _title.text = LocalizationConstants.FinishTitleRu;
                    break;
                case Language.TR:
                    _addCounsButtonText.text = LocalizationConstants.AddCoinsTr;
                    _nextLevelButtonText.text = LocalizationConstants.NextLevelTr;
                    _title.text = LocalizationConstants.FinishTitleTr;
                    break;
                default:
                    _addCounsButtonText.text = LocalizationConstants.AddCoinsEn;
                    _nextLevelButtonText.text = LocalizationConstants.NextLevelEn;
                    _title.text = LocalizationConstants.FinishTitleEn;
                    break;
            }
        }
    }
}