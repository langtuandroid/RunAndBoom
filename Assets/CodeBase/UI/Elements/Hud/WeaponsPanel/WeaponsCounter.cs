using CodeBase.Data.Progress;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapons;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.WeaponsPanel
{
    public class WeaponsCounter : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TextMeshProUGUI _grenadeLaucher;
        [SerializeField] private TextMeshProUGUI _rpg;
        [SerializeField] private TextMeshProUGUI _rocketLaucher;
        [SerializeField] private TextMeshProUGUI _mortar;

        private ILocalizationService _localizationService;
        private ProgressData _progressData;

        private void OnEnable()
        {
            if (_progressData != null)
            {
                _progressData.WeaponsData.WeaponsAmmoData.GrenadeLauncherAmmoChanged +=
                    ChangeGrenadeLauncherAmmo;
                _progressData.WeaponsData.WeaponsAmmoData.RpgAmmoChanged += ChangeRpgAmmo;
                _progressData.WeaponsData.WeaponsAmmoData.RocketLauncherAmmoChanged +=
                    ChangeRocketLauncherAmmo;
                _progressData.WeaponsData.WeaponsAmmoData.MortarAmmoChanged += ChangeMortarAmmo;
            }

            if (_localizationService == null)
                _localizationService = AllServices.Container.Single<ILocalizationService>();

            _localizationService.LanguageChanged += RefreshAmmo;
        }

        private void OnDisable()
        {
            if (_progressData?.WeaponsData != null)
            {
                _progressData.WeaponsData.WeaponsAmmoData.GrenadeLauncherAmmoChanged -=
                    ChangeGrenadeLauncherAmmo;
                _progressData.WeaponsData.WeaponsAmmoData.RpgAmmoChanged -= ChangeRpgAmmo;
                _progressData.WeaponsData.WeaponsAmmoData.RocketLauncherAmmoChanged -=
                    ChangeRocketLauncherAmmo;
                _progressData.WeaponsData.WeaponsAmmoData.MortarAmmoChanged -= ChangeMortarAmmo;
            }

            if (_localizationService != null)
                _localizationService.LanguageChanged -= RefreshAmmo;
        }

        private void Construct()
        {
            _progressData.WeaponsData.WeaponsAmmoData.GrenadeLauncherAmmoChanged +=
                ChangeGrenadeLauncherAmmo;
            _progressData.WeaponsData.WeaponsAmmoData.RpgAmmoChanged += ChangeRpgAmmo;
            _progressData.WeaponsData.WeaponsAmmoData.RocketLauncherAmmoChanged += ChangeRocketLauncherAmmo;
            _progressData.WeaponsData.WeaponsAmmoData.MortarAmmoChanged += ChangeMortarAmmo;
            _progressData.WeaponsData.WeaponsAmmoData.AmmoChanged(HeroWeaponTypeId.GrenadeLauncher);
            _progressData.WeaponsData.WeaponsAmmoData.AmmoChanged(HeroWeaponTypeId.RPG);
            _progressData.WeaponsData.WeaponsAmmoData.AmmoChanged(HeroWeaponTypeId.RocketLauncher);
            _progressData.WeaponsData.WeaponsAmmoData.AmmoChanged(HeroWeaponTypeId.Mortar);
        }

        private void ChangeGrenadeLauncherAmmo(int ammo) =>
            _grenadeLaucher.text = $"{ammo}";

        private void ChangeRpgAmmo(int ammo) =>
            _rpg.text = $"{ammo}";

        private void ChangeRocketLauncherAmmo(int ammo) =>
            _rocketLaucher.text = $"{ammo}";

        private void ChangeMortarAmmo(int ammo) =>
            _mortar.text = $"{ammo}";

        private void RefreshAmmo()
        {
            ChangeGrenadeLauncherAmmo(
                _progressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.GrenadeLauncher]);
            ChangeRpgAmmo(_progressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.RPG]);
            ChangeRocketLauncherAmmo(
                _progressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.RocketLauncher]);
            ChangeMortarAmmo(_progressData.WeaponsData.WeaponsAmmoData.Amunition.Dictionary[HeroWeaponTypeId.Mortar]);
        }

        public void LoadProgressData(ProgressData progressData)
        {
            _progressData = progressData;
            Construct();
        }
    }
}