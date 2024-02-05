using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Elements.Hud.MobileInputPanel;
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

        private const int MIN_GRENADE_LAUNCHER_AMMO_COUNT = 4;
        private const int MIN_RPG_AMMO_COUNT = 2;
        private const int MIN_ROCKET_LAUNCHER_AMMO_COUNT = 3;
        private const int MIN_MORTAR_AMMO_COUNT = 1;

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

        public void Construct(GameObject hero, OpenSettings openSettings, MobileInput mobileInput) =>
            base.Construct(hero, WindowId.Death, openSettings, mobileInput);

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
            TryAddAmmo();
            Hide();
        }

        private void RecoverForAds()
        {
            Recover();
            SoundInstance.StartRandomMusic();
        }

        private void RecoverHealth() =>
            Hero.GetComponent<HeroHealth>().Recover();

        private void TryAddAmmo()
        {
            int additionalAmmo;

            if (ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.GrenadeLauncher] <
                InputService.GetCount(MIN_GRENADE_LAUNCHER_AMMO_COUNT))
            {
                additionalAmmo = InputService.GetCount(MIN_GRENADE_LAUNCHER_AMMO_COUNT) -
                                 ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[
                                     HeroWeaponTypeId.GrenadeLauncher];
                ProgressData.WeaponsData.WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.GrenadeLauncher, additionalAmmo);
            }

            if (ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.RPG] <
                InputService.GetCount(MIN_RPG_AMMO_COUNT))
            {
                additionalAmmo = InputService.GetCount(MIN_RPG_AMMO_COUNT) -
                                 ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[
                                     HeroWeaponTypeId.RPG];
                ProgressData.WeaponsData.WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.RPG, additionalAmmo);
            }

            if (ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.RocketLauncher] <
                InputService.GetCount(MIN_ROCKET_LAUNCHER_AMMO_COUNT))
            {
                additionalAmmo = InputService.GetCount(MIN_ROCKET_LAUNCHER_AMMO_COUNT) -
                                 ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[
                                     HeroWeaponTypeId.RocketLauncher];
                ProgressData.WeaponsData.WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.RocketLauncher, additionalAmmo);
            }

            if (ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.Mortar] <
                InputService.GetCount(MIN_MORTAR_AMMO_COUNT))
            {
                additionalAmmo = InputService.GetCount(MIN_MORTAR_AMMO_COUNT) -
                                 ProgressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[
                                     HeroWeaponTypeId.Mortar];
                ProgressData.WeaponsData.WeaponsAmmoData.AddAmmo(HeroWeaponTypeId.Mortar, additionalAmmo);
            }
        }

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.Death), transform: transform,
                Volume, AudioSource);
        }
    }
}