using CodeBase.Hero;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.MobileInputPanel;
using CodeBase.UI.Elements.Hud.MobileInputPanel.Joysticks;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _recoverForAdsButton;
        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            if (!Application.isEditor)
                _restartButton.enabled = false;

            _recoverForAdsButton.onClick.AddListener(ShowAds);
            _restartButton.onClick.AddListener(RestartLevel);

            if (Application.isEditor)
                return;

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess += AdsServiceInitializedSuccess;
            AdsService.OnClosedVideoAd += RecoverForAds;
            AdsService.OnShowVideoAdError += ShowError;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            _recoverForAdsButton.onClick.RemoveListener(ShowAds);
            _restartButton.onClick.RemoveListener(RestartLevel);

            if (AdsService == null)
                return;

            AdsService.OnInitializeSuccess -= AdsServiceInitializedSuccess;
            AdsService.OnClosedVideoAd -= RecoverForAds;
            AdsService.OnShowVideoAdError -= ShowError;
        }

        public void Construct(GameObject hero, OpenSettings openSettings, MobileInput mobileInput,
            MoveJoystick moveJoystick, LookJoystick lookJoystick) =>
            base.Construct(hero, WindowId.Death, openSettings, mobileInput, moveJoystick, lookJoystick);

        protected override void AdsServiceInitializedSuccess()
        {
            base.AdsServiceInitializedSuccess();
            _restartButton.enabled = true;
        }

        private void ShowAds()
        {
            if (Application.isEditor)
            {
                Recover();
            }
            else
            {
                AdsService.ShowVideoAd();
                SoundInstance.StopRandomMusic(false);
            }
        }

        private void ShowError(string message) =>
            Debug.Log($"OnErrorFullScreen: {message}");

        private void Recover()
        {
            RecoverHealth();
            Hide();
        }

        private void RecoverForAds()
        {
            Recover();
            SoundInstance.StartRandomMusic();
        }

        private void RecoverHealth() =>
            Hero.GetComponent<HeroHealth>().Recover();

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.Death), transform: transform,
                Volume, AudioSource);
        }
    }
}