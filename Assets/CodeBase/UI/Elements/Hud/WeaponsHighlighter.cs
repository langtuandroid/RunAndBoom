using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements.Hud
{
    public class WeaponsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _grenadeLaucher;
        [SerializeField] private GameObject _rpg;
        [SerializeField] private GameObject _rocketLaucher;
        [SerializeField] private GameObject _mortar;

        private IPlayerProgressService _progressService;

        [Inject]
        public void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        private void Awake() =>
            _progressService.Progress.WeaponsData.ChangedHeroWeapon += HighlightWeapon;

        private void OnDisable() =>
            _progressService.Progress.WeaponsData.ChangedHeroWeapon -= HighlightWeapon;

        private void HighlightWeapon()
        {
            switch (_progressService.Progress.WeaponsData.CurrentHeroWeaponTypeId)
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