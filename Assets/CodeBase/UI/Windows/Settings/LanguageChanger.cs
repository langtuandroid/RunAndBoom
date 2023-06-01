using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class LanguageChanger : MonoBehaviour
    {
        [SerializeField] private Button _ruButton;
        [SerializeField] private Button _trButton;
        [SerializeField] private Button _enButton;
        [SerializeField] private GameObject _ruSelection;
        [SerializeField] private GameObject _trSelection;
        [SerializeField] private GameObject _enSelection;

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

        private void RuClicked() =>
            _localizationService.ChangeLanguage(Language.RU);

        private void TrClicked() =>
            _localizationService.ChangeLanguage(Language.TR);

        private void EnClicked() =>
            _localizationService.ChangeLanguage(Language.EN);

        private void ChangeText()
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
    }
}