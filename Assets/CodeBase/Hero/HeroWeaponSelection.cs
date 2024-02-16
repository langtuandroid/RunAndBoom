using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Projectiles;
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
        private ProgressData _progressData;
        private bool _canSelect;
        private int _currentWeapon;
        private List<HeroWeaponTypeId> _heroWeaponTypeIds;
        private HeroReloading _heroReloading;
        private HeroDeath _death;

        public event Action<GameObject, HeroWeaponStaticData, TrailStaticData> WeaponSelected;

        private void Awake()
        {
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            _heroWeaponTypeIds = DataExtensions.GetValues<HeroWeaponTypeId>().ToList();
        }

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

        public void Construct(HeroDeath death, HeroReloading heroReloading)
        {
            _death = death;
            _heroReloading = heroReloading;
            InitializeWeaponsDictionary();
        }

        private void InitializeWeaponsDictionary()
        {
            _weaponsDictionary = new Dictionary<HeroWeaponTypeId, GameObject>(_weapons.Length);
            _weaponsDictionary.Add(HeroWeaponTypeId.GrenadeLauncher, _weapons[0]);
            _weaponsDictionary.Add(HeroWeaponTypeId.RPG, _weapons[1]);
            _weaponsDictionary.Add(HeroWeaponTypeId.RocketLauncher, _weapons[2]);
            _weaponsDictionary.Add(HeroWeaponTypeId.Mortar, _weapons[3]);

            foreach (var keyValue in _weaponsDictionary)
                keyValue.Value.GetComponent<HeroWeaponAppearance>().Construct(_death, _heroReloading, this);
        }

        public void TurnOn() =>
            _canSelect = true;

        public void TurnOff() =>
            _canSelect = false;

        public void SelectWeapon(HeroWeaponTypeId heroWeaponTypeId)
        {
            Debug.Log($"heroWeaponTypeId: {heroWeaponTypeId}");
            if (_currentWeapon != _heroWeaponTypeIds.IndexOf(heroWeaponTypeId))
                FindWeaponContainer(heroWeaponTypeId);
        }

        private void FindWeaponContainer(HeroWeaponTypeId heroWeaponTypeId)
        {
            if (!_progressData.WeaponsData.WeaponData.First(x => x.WeaponTypeId == heroWeaponTypeId).IsAvailable)
                return;

            GameObject weapon = _weaponsDictionary.First(x => x.Key == heroWeaponTypeId).Value;
            weapon.GetComponent<HeroWeaponAppearance>().ReturnShotsVfx();

            foreach (var keyValue in _weaponsDictionary)
                keyValue.Value.SetActive(keyValue.Key == heroWeaponTypeId);

            WeaponChosen(heroWeaponTypeId);
        }

        private void WeaponChosen(HeroWeaponTypeId heroWeaponTypeId)
        {
            _progressData.WeaponsData.SetCurrentWeapon(heroWeaponTypeId);
            _currentWeapon = _heroWeaponTypeIds.IndexOf(heroWeaponTypeId);
            HeroWeaponStaticData heroWeaponStaticData = _staticDataService.ForHeroWeapon(heroWeaponTypeId);
            TrailStaticData trailStaticData = _staticDataService.ForTrail(heroWeaponStaticData.TrailTypeId);
            WeaponSelected?.Invoke(_weaponsDictionary[heroWeaponTypeId], heroWeaponStaticData, trailStaticData);
        }

        public void LoadProgressData(ProgressData progressData)
        {
            _progressData = progressData;
            FindWeaponContainer(_progressData.WeaponsData.CurrentHeroWeaponTypeId);
        }
    }
}