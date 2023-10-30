using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class LanguageChanger : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Button _ruButton;
        [SerializeField] private Button _trButton;
        [SerializeField] private Button _enButton;
        [SerializeField] private GameObject _ruSelection;
        [SerializeField] private GameObject _trSelection;
        [SerializeField] private GameObject _enSelection;

        private ILocalizationService _localizationService;
        private ISaveLoadService _saveLoadService;

        private void OnEnable()
        {
            _ruButton.onClick.AddListener(RuClicked);
            _trButton.onClick.AddListener(TrClicked);
            _enButton.onClick.AddListener(EnClicked);

            if (_localizationService == null)
                _localizationService = AllServices.Container.Single<ILocalizationService>();

            if (_saveLoadService == null)
                _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            if (_localizationService != null)
            {
                _localizationService.LanguageChanged += ChangeHighlighting;
                ChangeHighlighting();
            }
        }

        private void OnDisable()
        {
            _ruButton.onClick.RemoveListener(RuClicked);
            _trButton.onClick.RemoveListener(TrClicked);
            _enButton.onClick.RemoveListener(EnClicked);

            if (_localizationService != null)
                _localizationService.LanguageChanged -= ChangeHighlighting;
        }

        private void RuClicked()
        {
            _localizationService.ChangeLanguage(Language.RU);
            _saveLoadService.SaveLanguage(Language.RU);
        }

        private void TrClicked()
        {
            _localizationService.ChangeLanguage(Language.TR);
            _saveLoadService.SaveLanguage(Language.TR);
        }

        private void EnClicked()
        {
            _localizationService.ChangeLanguage(Language.EN);
            _saveLoadService.SaveLanguage(Language.EN);
        }

        private void ChangeHighlighting()
        {
            switch (_localizationService.Language)
            {
                case Language.RU:
                    _ruSelection.SetActive(true);
                    _trSelection.SetActive(false);
                    _enSelection.SetActive(false);
                    break;
                case Language.TR:
                    _trSelection.SetActive(true);
                    _ruSelection.SetActive(false);
                    _enSelection.SetActive(false);
                    break;
                default:
                    _enSelection.SetActive(true);
                    _ruSelection.SetActive(false);
                    _trSelection.SetActive(false);
                    break;
            }
        }

        public void LoadProgressData(ProgressData progressData)
        {
            _localizationService.ChangeLanguage(AllServices.Container.Single<IPlayerProgressService>().SettingsData
                .Language);
        }
    }
}