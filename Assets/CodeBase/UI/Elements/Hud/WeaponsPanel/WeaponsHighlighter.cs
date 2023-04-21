using CodeBase.Hero;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.WeaponsPanel
{
    public class WeaponsHighlighter : MonoBehaviour
    {
        [SerializeField] private GameObject _grenadeLaucher;
        [SerializeField] private GameObject _rpg;
        [SerializeField] private GameObject _rocketLaucher;
        [SerializeField] private GameObject _mortar;
        [SerializeField] private Sprite _selected;
        [SerializeField] private Sprite _unselected;

        private HeroWeaponSelection _heroWeaponSelection;

        public void Construct(HeroWeaponSelection heroWeaponSelection)
        {
            _heroWeaponSelection = heroWeaponSelection;
            _heroWeaponSelection.WeaponSelected += HighlightWeapon;
        }

        private void HighlightWeapon(GameObject o, HeroWeaponStaticData heroWeaponStaticData,
            TrailStaticData trailStaticData)
        {
            switch (heroWeaponStaticData.WeaponTypeId)
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