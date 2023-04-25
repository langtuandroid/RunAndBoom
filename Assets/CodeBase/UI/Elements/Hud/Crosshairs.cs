using CodeBase.Hero;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud
{
    public class Crosshairs : MonoBehaviour
    {
        [SerializeField] private GameObject _grenadeLauncher;
        [SerializeField] private GameObject _rpg;
        [SerializeField] private GameObject _rocketLauncher;
        [SerializeField] private GameObject _mortar;

        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;
        private HeroReloading _heroReloading;

        public void Construct(HeroReloading heroShooting, HeroWeaponSelection heroWeaponSelection)
        {
            _heroReloading = heroShooting;
            _heroWeaponSelection = heroWeaponSelection;

            _heroReloading.OnStartReloading += Hide;
            _heroReloading.OnStopReloading += Show;
            _heroWeaponSelection.WeaponSelected += ChangeCrosshair;
        }

        private void ChangeCrosshair(GameObject arg1, HeroWeaponStaticData weaponStaticData, TrailStaticData arg3)
        {
            _heroWeaponTypeId = weaponStaticData.WeaponTypeId;
            Show();
        }

        private void Hide(float f)
        {
            _grenadeLauncher.SetActive(false);
            _rpg.SetActive(false);
            _rocketLauncher.SetActive(false);
            _mortar.SetActive(false);
        }

        private void Show()
        {
            switch (_heroWeaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    _grenadeLauncher.SetActive(true);
                    _rpg.SetActive(false);
                    _rocketLauncher.SetActive(false);
                    _mortar.SetActive(false);
                    break;

                case HeroWeaponTypeId.RPG:
                    _rpg.SetActive(true);
                    _grenadeLauncher.SetActive(false);
                    _rocketLauncher.SetActive(false);
                    _mortar.SetActive(false);
                    break;

                case HeroWeaponTypeId.RocketLauncher:
                    _rocketLauncher.SetActive(true);
                    _grenadeLauncher.SetActive(false);
                    _rpg.SetActive(false);
                    _mortar.SetActive(false);
                    break;

                case HeroWeaponTypeId.Mortar:
                    _mortar.SetActive(true);
                    _rocketLauncher.SetActive(false);
                    _grenadeLauncher.SetActive(false);
                    _rpg.SetActive(false);
                    break;
            }
        }
    }
}