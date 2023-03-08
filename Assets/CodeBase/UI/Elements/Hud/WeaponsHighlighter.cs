using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements.Hud
{
    public class WeaponsHighlighter : MonoBehaviour, IProgressReader
    {
        [SerializeField] private GameObject _grenadeLaucher;
        [SerializeField] private GameObject _rpg;
        [SerializeField] private GameObject _rocketLaucher;
        [SerializeField] private GameObject _mortar;

        // private IPlayerProgressService _progressService;
        private PlayerProgress _progress;

        // [Inject]
        // public void Construct(IPlayerProgressService progressService)
        // {
        //     _progressService = progressService;
        //     Subscribe();
        // }

        // private void Awake() =>

        private void OnDisable() =>
            _progress.WeaponsData.HeroWeaponChanged -= HighlightWeapon;
        // _progressService.Progress.WeaponsData.HeroWeaponChanged -= HighlightWeapon;

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.WeaponsData.HeroWeaponChanged += HighlightWeapon;
        }

        private void HighlightWeapon()
        {
            switch (_progress.WeaponsData.CurrentHeroWeaponTypeId)
                // switch (_progressService.Progress.WeaponsData.CurrentHeroWeaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    _grenadeLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaSelectedItem);
                    _rpg.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _rocketLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _mortar.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    break;

                case HeroWeaponTypeId.RPG:
                    _rpg.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaSelectedItem);
                    _grenadeLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _rocketLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _mortar.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    break;

                case HeroWeaponTypeId.RocketLauncher:
                    _rocketLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaSelectedItem);
                    _grenadeLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _rpg.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _mortar.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    break;

                case HeroWeaponTypeId.Mortar:
                    _mortar.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaSelectedItem);
                    _grenadeLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _rpg.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    _rocketLaucher.GetComponent<Image>().ChangeImageAlpha(Constants.AlphaUnselectedItem);
                    break;
            }
        }
    }
}