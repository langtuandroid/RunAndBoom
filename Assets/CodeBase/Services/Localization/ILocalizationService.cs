using System;
using CodeBase.Data.Settings;

namespace CodeBase.Services.Localization
{
    public interface ILocalizationService: IService
    {
        Language Language { get; }
        
        public event Action LanguageChanged;
        
        string GetText(string russian, string turkish, string english);
        void ChangeLanguage(Language language);
    }
}