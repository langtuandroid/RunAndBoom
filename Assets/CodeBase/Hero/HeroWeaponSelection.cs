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

        public event Action<GameObject, HeroWeaponStaticData, ProjectileTraceStaticData> WeaponSelected;

        [Inject]
        public void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void LoadProgress(PlayerProgress progress)
        {
            FindWeaponContainer(progress.CurrentHeroWeaponTypeId);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        private void FindWeaponContainer(HeroWeaponTypeId heroWeaponTypeId)
        {
            foreach (GameObject weapon in _weapons)
            {
                if (weapon.name == heroWeaponTypeId.ToString())
                {
                    weapon.GetComponent<HeroWeaponAppearance>().Construct(this);
                    weapon.SetActive(true);
                    WeaponChosen(weapon, heroWeaponTypeId);
                }
                else
                    weapon.SetActive(false);
            }
        }

        private void WeaponChosen(GameObject currentWeapon, HeroWeaponTypeId heroWeaponTypeId)
        {
            HeroWeaponStaticData heroWeaponStaticData = _staticDataService.ForHeroWeapon(heroWeaponTypeId);
            ProjectileTraceStaticData projectileTraceStaticData = _staticDataService.ForProjectileTrace(heroWeaponStaticData.ProjectileTraceTypeId);
            WeaponSelected?.Invoke(currentWeapon, heroWeaponStaticData, projectileTraceStaticData);
        }
    }
}