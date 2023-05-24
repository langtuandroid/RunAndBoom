using System.Collections;
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

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = MaximumAlpha;
        }

        public void Hide()
        {
            SoundInstance.SetStartFade(1f);
            SoundInstance.StartRandomMusic();
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (_curtain.alpha > MinimumAlpha)
            {
                _curtain.alpha -= StepAlpha;
                yield return new WaitForSeconds(StepAlpha);
            }

            gameObject.SetActive(false);
        }
    }
}