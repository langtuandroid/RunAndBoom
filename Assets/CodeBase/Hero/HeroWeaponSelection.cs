using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Weapon;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroWeaponSelection : MonoBehaviour, IProgressSaver
    {
        private const string WeaponContainerName = "Weapon";

        private IStaticDataService _staticDataService;

        private WeaponTypeId _currentWeaponTypeId;
        private Transform _transform;
        private WeaponStaticData _currentWeaponStaticData;

        public event Action<WeaponStaticData, Transform> WeaponSelected;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;

            _currentWeaponStaticData = _staticDataService.ForWeapon(_currentWeaponTypeId);
            FindWeaponContainer();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _currentWeaponTypeId = progress.CurrentWeaponTypeId;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        private void FindWeaponContainer()
        {
            _transform = transform;

            for (int i = 0; i < _transform.childCount; i++)
            {
                if (_transform.GetChild(i).gameObject.name == WeaponContainerName)
                {
                    _transform = _transform.GetChild(i).gameObject.transform;

                    FindWeapon();
                    break;
                }
            }
        }

        private void FindWeapon()
        {
            string weaponName = _currentWeaponStaticData.WeaponTypeId.ToString();


            for (int i = 0; i < _transform.childCount; i++)
            {
                if (_transform.GetChild(i).gameObject.name == weaponName)
                    SetWeaponVisible(i);
                else
                    _transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void SetWeaponVisible(int i)
        {
            _transform.GetChild(i).gameObject.SetActive(true);
            Transform current = _transform.GetChild(i).gameObject.transform;
            WeaponSelected?.Invoke(_currentWeaponStaticData, current);
        }
    }
}