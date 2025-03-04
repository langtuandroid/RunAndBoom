﻿using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Weapons;
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

        private ProgressData _progressData;

        private void ShowAvailable()
        {
            foreach (WeaponData weaponsData in _progressData.WeaponsData.WeaponData)
                if (weaponsData.IsAvailable)
                    SetVisibility(weaponsData.WeaponTypeId, true);
                else
                    SetVisibility(weaponsData.WeaponTypeId, false);
        }

        private void SetVisibility(HeroWeaponTypeId typeId, bool isVisible)
        {
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    if (_grenadeLauncher.activeInHierarchy != isVisible)
                        _grenadeLauncher.SetActive(isVisible);
                    break;
                case HeroWeaponTypeId.RPG:
                    if (_rpg.activeInHierarchy != isVisible)
                        _rpg.SetActive(isVisible);
                    break;
                case HeroWeaponTypeId.RocketLauncher:
                    if (_rocketLauncher.activeInHierarchy != isVisible)
                        _rocketLauncher.SetActive(isVisible);
                    break;
                case HeroWeaponTypeId.Mortar:
                    if (_mortar.activeInHierarchy != isVisible)
                        _mortar.SetActive(isVisible);
                    break;
            }
        }

        public void ShowAll()
        {
            if (!_grenadeLauncher.activeInHierarchy)
                _grenadeLauncher.SetActive(true);

            if (!_rpg.activeInHierarchy)
                _rpg.SetActive(true);

            if (!_rocketLauncher.activeInHierarchy)
                _rocketLauncher.SetActive(true);

            if (!_mortar.activeInHierarchy)
                _mortar.SetActive(true);
        }

        public void LoadProgressData(ProgressData progressData)
        {
            _progressData = progressData;
            _progressData.WeaponsData.SetAvailable += Show;
            ShowAvailable();
        }

        private void Show(HeroWeaponTypeId typeId) =>
            SetVisibility(typeId, true);
    }
}