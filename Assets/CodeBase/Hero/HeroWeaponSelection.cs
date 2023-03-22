using System;
using System.Collections.Generic;
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

        private Dictionary<HeroWeaponTypeId, GameObject> _weaponsDictionary;
        private IStaticDataService _staticDataService;
        private PlayerProgress _progress;
        private bool _canSelect;
        private int _currentWeapon;
        private List<HeroWeaponTypeId> _heroWeaponTypeIds;

        public event Action<GameObject, HeroWeaponStaticData, ProjectileTraceStaticData> WeaponSelected;

        private void Awake()
        {
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            _heroWeaponTypeIds = DataExtensions.GetValues<HeroWeaponTypeId>().ToList();

            InitializeWeaponsDictionary();
        }

        private void InitializeWeaponsDictionary()
        {
            _weaponsDictionary = new Dictionary<HeroWeaponTypeId, GameObject>(_weapons.Length);
            _weaponsDictionary.Add(HeroWeaponTypeId.GrenadeLauncher, _weapons[0]);
            _weaponsDictionary.Add(HeroWeaponTypeId.RPG, _weapons[1]);
            _weaponsDictionary.Add(HeroWeaponTypeId.RocketLauncher, _weapons[2]);
            _weaponsDictionary.Add(HeroWeaponTypeId.Mortar, _weapons[3]);
        }

        public void TurnOn() =>
            _canSelect = true;

        public void TurnOff() =>
            _canSelect = false;

        private void Update()
        {
            if (_canSelect)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    SelectWeapon(HeroWeaponTypeId.GrenadeLauncher);

                if (Input.GetKeyDown(KeyCode.Alpha2))
                    SelectWeapon(HeroWeaponTypeId.RPG);

                if (Input.GetKeyDown(KeyCode.Alpha3))
                    SelectWeapon(HeroWeaponTypeId.RocketLauncher);

                if (Input.GetKeyDown(KeyCode.Alpha4))
                    SelectWeapon(HeroWeaponTypeId.Mortar);
            }
        }

        private void SelectWeapon(HeroWeaponTypeId heroWeaponTypeId)
        {
            if (_currentWeapon != _heroWeaponTypeIds.IndexOf(heroWeaponTypeId))
                FindWeaponContainer(heroWeaponTypeId);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            FindWeaponContainer(_progress.WeaponsData.CurrentHeroWeaponTypeId);
            _canSelect = true;
        }

        private void FindWeaponContainer(HeroWeaponTypeId heroWeaponTypeId)
        {
            if (!_progress.WeaponsData.WeaponDatas.First(x => x.WeaponTypeId == heroWeaponTypeId).IsAvailable)
                return;

            foreach (var keyValue in _weaponsDictionary)
            {
                if (keyValue.Key == heroWeaponTypeId)
                {
                    keyValue.Value.GetComponent<HeroWeaponAppearance>().Construct(this);
                    keyValue.Value.SetActive(true);
                    WeaponChosen(heroWeaponTypeId);
                }
                else
                {
                    keyValue.Value.SetActive(false);
                }
            }
        }

        private void WeaponChosen(HeroWeaponTypeId heroWeaponTypeId)
        {
            _progress.WeaponsData.SetCurrentWeapon(heroWeaponTypeId);
            _currentWeapon = _heroWeaponTypeIds.IndexOf(heroWeaponTypeId);
            HeroWeaponStaticData heroWeaponStaticData = _staticDataService.ForHeroWeapon(heroWeaponTypeId);
            ProjectileTraceStaticData projectileTraceStaticData = _staticDataService.ForProjectileTrace(heroWeaponStaticData.ProjectileTraceTypeId);
            WeaponSelected?.Invoke(_weaponsDictionary[heroWeaponTypeId], heroWeaponStaticData, projectileTraceStaticData);
        }
    }
}