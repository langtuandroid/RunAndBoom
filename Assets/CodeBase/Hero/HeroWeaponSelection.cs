using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroWeaponSelection : MonoBehaviour, IProgressSaver
    {
        [SerializeField] private GameObject[] _weapons;

        private IStaticDataService _staticDataService;

        public event Action<GameObject, WeaponStaticData, ProjectileTraceStaticData> WeaponSelected;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            FindWeaponContainer(progress.CurrentWeaponTypeId);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        private void FindWeaponContainer(WeaponTypeId weaponTypeId)
        {
            foreach (GameObject weapon in _weapons)
            {
                if (weapon.name == weaponTypeId.ToString())
                {
                    weapon.GetComponent<WeaponAppearance>().Construct(this);
                    weapon.SetActive(true);
                    WeaponChosen(weapon, weaponTypeId);
                }
                else
                    weapon.SetActive(false);
            }
        }

        private void WeaponChosen(GameObject currentWeapon, WeaponTypeId weaponTypeId)
        {
            WeaponStaticData weaponStaticData = _staticDataService.ForWeapon(weaponTypeId);
            ProjectileTraceStaticData projectileTraceStaticData = _staticDataService.ForProjectileTrace(weaponStaticData.ProjectileTraceTypeId);
            WeaponSelected?.Invoke(currentWeapon, weaponStaticData, projectileTraceStaticData);
        }
    }
}