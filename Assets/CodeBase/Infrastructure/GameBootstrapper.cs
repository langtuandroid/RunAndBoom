using CodeBase.Data.Settings;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Localization;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private AudioBackgroundChanger _audioBackgroundChangerPrefab;

        private Game _game;

        private void Awake()
        {
            Language language = GetLanguage();
            LoadingCurtain loadingCurtain = Instantiate(_curtainPrefab);
            SetLoadingText(language, loadingCurtain);

            _game = new Game(this, loadingCurtain, language);
            _game.StateMachine.Enter<BootstrapState>();
            Instantiate(_audioBackgroundChangerPrefab);

            DontDestroyOnLoad(this);
        }

        private void SetLoadingText(Language language, LoadingCurtain loadingCurtain)
        {
            switch (language)
            {
                case Language.RU:
                    loadingCurtain.GetComponentInChildren<Text>().text = LocalizationConstants.LoadingRu;
                    break;
                case Language.TR:
                    loadingCurtain.GetComponentInChildren<Text>().text = LocalizationConstants.LoadingTr;
                    break;
                case Language.EN:
                    loadingCurtain.GetComponentInChildren<Text>().text = LocalizationConstants.LoadingEn;
                    break;
            }
        }

        private Language GetLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    return Language.RU;
                case SystemLanguage.Turkish:
                    return Language.TR;
                default:
                    return Language.EN;
            }
        }
    }
}