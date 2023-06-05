using System;
using System.Collections;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;
        [SerializeField] private HeroReloading _heroReloading;

        private const float ShootDelay = 0.1f;

        private IPlayerProgressService _progressService;
        private HeroWeaponAppearance _heroWeaponAppearance;
        private bool _canShoot = false;

        public event Action Shot;

        public void TurnOn() =>
            StartCoroutine(EnableShoot());

        public void TurnOff() =>
            _canShoot = false;

        private IEnumerator EnableShoot()
        {
            yield return new WaitForSeconds(ShootDelay);
            _canShoot = true;
        }

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            _heroReloading.OnStopReloading += TurnOn;
        }

        private void GetCurrentWeaponObject(GameObject weaponPrefab, HeroWeaponStaticData heroWeaponStaticData,
            TrailStaticData trailStaticData)
        {
            _heroWeaponAppearance = weaponPrefab.GetComponent<HeroWeaponAppearance>();
            TurnOn();
        }

        private void Update()
        {
            if (_canShoot)
                if (Input.GetMouseButton(0))
                    TryShoot();
        }

        private void TryShoot()
        {
            if (_canShoot && IsAvailableAmmo())
                Shoot();
        }

        private bool IsAvailableAmmo()
        {
            return _progressService.Progress.WeaponsData.WeaponsAmmoData.IsAmmoAvailable();
        }

        private void Shoot()
        {
            Shot?.Invoke();
            TurnOff();
            _progressService.Progress.WeaponsData.WeaponsAmmoData.ReduceAmmo();
            _heroWeaponAppearance.ShootTo();
        }
    }
}