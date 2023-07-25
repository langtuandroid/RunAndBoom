using System.Collections;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup _curtain;

        private const int MinimumAlpha = 0;
        private const int MaximumAlpha = 1;
        private const float StepAlpha = 0.03f;
        private const float PrepareWaiting = 2f;
        private bool _isInitial = true;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = MaximumAlpha;
        }

        public void Hide()
        {
            SoundInstance.SetStartFade();
            SoundInstance.StartRandomMusic();
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(PrepareWaiting);

            while (_curtain.alpha > MinimumAlpha)
            {
                _curtain.alpha -= StepAlpha;
                yield return new WaitForSeconds(StepAlpha);
            }

            ShowAd();
            AllServices.Container.Single<ISaveLoadService>().SaveProgress();
            gameObject.SetActive(false);
        }

        private void ShowAd()
        {
            IPlayerProgressService playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            Debug.Log($"ShowAd {playerProgressService.Progress.WorldData.ShowAdOnLevelStart}");

            if (playerProgressService.Progress.WorldData.ShowAdOnLevelStart)
            {
                playerProgressService.Progress.WorldData.ShowAdOnLevelStart = false;

                if (Application.isEditor)
                    return;

                SoundInstance.StopRandomMusic(false);
                AllServices.Container.Single<IAdsService>().ShowInterstitialAd();
            }
        }
    }
}