using System;
using System.Collections;
using CodeBase.Services;
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
        private ISaveLoadService _saveLoadService;
        private WaitForSeconds _waitForSeconds;
        private WaitForSeconds _forSeconds;

        public event Action FadedOut;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _waitForSeconds = new WaitForSeconds(PrepareWaiting);
            _forSeconds = new WaitForSeconds(StepAlpha);
        }

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
            yield return _waitForSeconds;

            while (_curtain.alpha > MinimumAlpha)
            {
                _curtain.alpha -= StepAlpha;
                yield return _forSeconds;
            }

            FadedOut?.Invoke();
            SaveData();
            gameObject.SetActive(false);
        }

        private void SaveData()
        {
            if (_saveLoadService == null)
                _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            _saveLoadService.SaveProgressData();
            _saveLoadService.SaveSettingsData();
        }
    }
}