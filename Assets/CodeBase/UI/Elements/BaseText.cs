using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public abstract class BaseText : MonoBehaviour
    {
        private ILocalizationService _localizationService;

        private void Awake()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _localizationService.LanguageChanged += ChangeText;
            ChangeText();
        }

        protected abstract void RuChosen();
        protected abstract void TrChosen();
        protected abstract void EnChosen();

        private void ChangeText()
        {
            switch (_localizationService.Language)
            {
                case Language.RU:
                    RuChosen();
                    break;
                case Language.TR:
                    TrChosen();
                    break;
                default:
                    EnChosen();
                    break;
            }
        }
    }
}