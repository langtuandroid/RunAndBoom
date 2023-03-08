using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud
{
    public class WeaponsHighlighter : MonoBehaviour, IProgressReader
    {
        [SerializeField] private GameObject _grenadeLaucher;
        [SerializeField] private GameObject _rpg;
        [SerializeField] private GameObject _rocketLaucher;
        [SerializeField] private GameObject _mortar;
        [SerializeField] private Sprite _selected;
        [SerializeField] private Sprite _unselected;

        private PlayerProgress _progress;

        private void OnDisable() =>
            _progress.WeaponsData.HeroWeaponChanged -= HighlightWeapon;

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.WeaponsData.HeroWeaponChanged += HighlightWeapon;
            HighlightWeapon();
        }

        private void HighlightWeapon()
        {
            switch (_progress.WeaponsData.CurrentHeroWeaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    _grenadeLaucher.GetComponent<Image>().sprite = _selected;
                    _rpg.GetComponent<Image>().sprite = _unselected;
                    _rocketLaucher.GetComponent<Image>().sprite = _unselected;
                    _mortar.GetComponent<Image>().sprite = _unselected;
                    break;

                case HeroWeaponTypeId.RPG:
                    _rpg.GetComponent<Image>().sprite = _selected;
                    _grenadeLaucher.GetComponent<Image>().sprite = _unselected;
                    _rocketLaucher.GetComponent<Image>().sprite = _unselected;
                    _mortar.GetComponent<Image>().sprite = _unselected;
                    break;

                case HeroWeaponTypeId.RocketLauncher:
                    _rocketLaucher.GetComponent<Image>().sprite = _selected;
                    _grenadeLaucher.GetComponent<Image>().sprite = _unselected;
                    _rpg.GetComponent<Image>().sprite = _unselected;
                    _mortar.GetComponent<Image>().sprite = _unselected;
                    break;

                case HeroWeaponTypeId.Mortar:
                    _mortar.GetComponent<Image>().sprite = _selected;
                    _grenadeLaucher.GetComponent<Image>().sprite = _unselected;
                    _rpg.GetComponent<Image>().sprite = _unselected;
                    _rocketLaucher.GetComponent<Image>().sprite = _unselected;
                    break;
            }
        }
    }
}