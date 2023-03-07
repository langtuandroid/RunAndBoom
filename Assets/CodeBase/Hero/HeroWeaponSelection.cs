using System;
using CodeBase.Data;
using CodeBase.Services.Input.Platforms;
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
        private IPlatformInputService _platformInputService = null;
        private HeroWeaponTypeId _heroWeaponTypeId;

        public event Action<GameObject, HeroWeaponStaticData, ProjectileTraceStaticData> WeaponSelected;

        [Inject]
        public void Construct(IStaticDataService staticDataService, IPlatformInputService platformInputService)
        {
            _staticDataService = staticDataService;

            if (platformInputService is DesktopPlatformInputService service)
                _platformInputService = service;

            if (platformInputService is EditorPlatformInputService inputService)
                _platformInputService = inputService;
        }

        private void Awake()
        {
            if (_platformInputService != null)
            {
                _platformInputService.ChoseWeapon1 += SelectWeapon1;
                _platformInputService.ChoseWeapon2 += SelectWeapon2;
                _platformInputService.ChoseWeapon3 += SelectWeapon3;
                _platformInputService.ChoseWeapon4 += SelectWeapon4;
            }
        }

        private void OnDestroy()
        {
            if (_platformInputService != null)
            {
                _platformInputService.ChoseWeapon1 -= SelectWeapon1;
                _platformInputService.ChoseWeapon2 -= SelectWeapon2;
                _platformInputService.ChoseWeapon3 -= SelectWeapon3;
                _platformInputService.ChoseWeapon4 -= SelectWeapon4;
            }
        }

        private void SelectWeapon1() =>
            FindWeaponContainer(HeroWeaponTypeId.GrenadeLauncher);

        private void SelectWeapon2() =>
            FindWeaponContainer(HeroWeaponTypeId.RPG);

        private void SelectWeapon3() =>
            FindWeaponContainer(HeroWeaponTypeId.RocketLauncher);

        private void SelectWeapon4() =>
            FindWeaponContainer(HeroWeaponTypeId.Mortar);

        public void LoadProgress(PlayerProgress progress)
        {
            FindWeaponContainer(progress.CurrentHeroWeaponTypeId);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CurrentHeroWeaponTypeId = _heroWeaponTypeId;
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
            _heroWeaponTypeId = heroWeaponTypeId;
            HeroWeaponStaticData heroWeaponStaticData = _staticDataService.ForHeroWeapon(heroWeaponTypeId);
            ProjectileTraceStaticData projectileTraceStaticData = _staticDataService.ForProjectileTrace(heroWeaponStaticData.ProjectileTraceTypeId);
            WeaponSelected?.Invoke(currentWeapon, heroWeaponStaticData, projectileTraceStaticData);
        }
    }
}