using CodeBase.Data;
using CodeBase.Data.Weapons;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.WeaponsPanel
{
    public class WeaponsVisibility : MonoBehaviour, IProgressReader
    {
        [SerializeField] private GameObject _grenadeLauncher;
        [SerializeField] private GameObject _rpg;
        [SerializeField] private GameObject _rocketLauncher;
        [SerializeField] private GameObject _mortar;

        private PlayerProgress _progress;

        private void Awake()
        {
            _grenadeLauncher.SetActive(false);
            _rpg.SetActive(false);
            _rocketLauncher.SetActive(false);
            _mortar.SetActive(false);
        }

        private void Construct()
        {
            _progress.WeaponsData.SetAvailable += WeaponIsAvailable;

            foreach (WeaponData weaponsData in _progress.WeaponsData.WeaponDatas)
                if (weaponsData.IsAvailable)
                    WeaponIsAvailable(weaponsData.WeaponTypeId);
        }

        private void WeaponIsAvailable(HeroWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    _grenadeLauncher.SetActive(true);
                    break;
                case HeroWeaponTypeId.RPG:
                    _rpg.SetActive(true);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    _rocketLauncher.SetActive(true);
                    break;
                case HeroWeaponTypeId.Mortar:
                    _mortar.SetActive(true);
                    break;
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            Construct();
        }
    }
}