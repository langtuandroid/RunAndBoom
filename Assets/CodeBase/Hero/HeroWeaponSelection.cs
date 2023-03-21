using System;
using System.Linq;
using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroWeaponSelection : MonoBehaviour, IProgressReader
    {
        [SerializeField] private GameObject[] _weapons;

        private IStaticDataService _staticDataService;
        private PlayerProgress _progress;
        private bool _canSelect = true;

        public event Action<GameObject, HeroWeaponStaticData, ProjectileTraceStaticData> WeaponSelected;

        private void Awake() =>
            _staticDataService = AllServices.Container.Single<IStaticDataService>();

        public void TurnOn() =>
            _canSelect = true;

        public void TurnOff() =>
            _canSelect = false;

        private void Update()
        {
            if (_canSelect)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    SelectWeapon1();

                if (Input.GetKeyDown(KeyCode.Alpha2))
                    SelectWeapon2();

                if (Input.GetKeyDown(KeyCode.Alpha3))
                    SelectWeapon3();

                if (Input.GetKeyDown(KeyCode.Alpha4))
                    SelectWeapon4();
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
            _progress = progress;
            FindWeaponContainer(_progress.WeaponsData.CurrentHeroWeaponTypeId);
        }

        private void FindWeaponContainer(HeroWeaponTypeId heroWeaponTypeId)
        {
            if (!_progress.WeaponsData.WeaponDatas.First(x => x.WeaponTypeId == heroWeaponTypeId).IsAvailable)
                return;

            foreach (GameObject weapon in _weapons)
            {
                if (weapon.name == heroWeaponTypeId.ToString())
                {
                    weapon.GetComponent<HeroWeaponAppearance>().Construct(this);
                    weapon.SetActive(true);
                    WeaponChosen(weapon, heroWeaponTypeId);
                }
                else
                {
                    weapon.SetActive(false);
                }
            }
        }

        private void WeaponChosen(GameObject currentWeapon, HeroWeaponTypeId heroWeaponTypeId)
        {
            _progress.WeaponsData.SetCurrentWeapon(heroWeaponTypeId);
            HeroWeaponStaticData heroWeaponStaticData = _staticDataService.ForHeroWeapon(heroWeaponTypeId);
            ProjectileTraceStaticData projectileTraceStaticData = _staticDataService.ForProjectileTrace(heroWeaponStaticData.ProjectileTraceTypeId);
            WeaponSelected?.Invoke(currentWeapon, heroWeaponStaticData, projectileTraceStaticData);
        }
    }
}