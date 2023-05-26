using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopText : MonoBehaviour
        //, IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _refreshButton;

        private PlayerProgress _progress;
        private TextMeshProUGUI _skipButtonText;
        private TextMeshProUGUI _refreshButtonText;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            _skipButtonText = _skipButton.GetComponentInChildren<TextMeshProUGUI>();
            _refreshButtonText = _refreshButton.GetComponentInChildren<TextMeshProUGUI>();
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
                    _skipButtonText.text = LocalizationConstants.SkipRu;
                    _refreshButtonText.text = LocalizationConstants.RefreshRu;
                    _title.text = LocalizationConstants.ShopTitleRu;
                    break;
                case Language.TR:
                    _skipButtonText.text = LocalizationConstants.SkipTr;
                    _refreshButtonText.text = LocalizationConstants.RefreshTr;
                    _title.text = LocalizationConstants.ShopTitleTr;
                    break;
                default:
                    _skipButtonText.text = LocalizationConstants.SkipEn;
                    _refreshButtonText.text = LocalizationConstants.RefreshEn;
                    _title.text = LocalizationConstants.ShopTitleEn;
                    break;
            }
        }
    }
}