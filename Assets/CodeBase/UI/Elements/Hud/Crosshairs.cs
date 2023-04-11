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

        private HeroWeaponSelection _heroWeaponSelection;
        private HeroWeaponTypeId _heroWeaponTypeId;
        private HeroShooting _heroShooting;

        public void Construct(HeroShooting heroShooting, HeroWeaponSelection heroWeaponSelection)
        {
            _heroShooting = heroShooting;
            _heroWeaponSelection = heroWeaponSelection;

            _heroShooting.OnStartReloading += Hide;
            _heroShooting.OnStopReloading += Show;
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
        }

        private void Show()
        {
            switch (_heroWeaponTypeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    _grenadeLauncher.SetActive(true);
                    _rpg.SetActive(false);
                    _rocketLauncher.SetActive(false);
                    break;

                case HeroWeaponTypeId.RPG:
                    _rpg.SetActive(true);
                    _grenadeLauncher.SetActive(false);
                    _rocketLauncher.SetActive(false);
                    break;

                case HeroWeaponTypeId.RocketLauncher:
                    _rocketLauncher.SetActive(true);
                    _grenadeLauncher.SetActive(false);
                    _rpg.SetActive(false);
                    break;

                case HeroWeaponTypeId.Mortar:
                    _rocketLauncher.SetActive(false);
                    _grenadeLauncher.SetActive(false);
                    _rpg.SetActive(false);
                    break;
            }
        }
    }
}