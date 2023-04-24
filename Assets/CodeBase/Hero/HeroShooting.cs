using System;
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

        private IPlayerProgressService _progressService;
        private HeroWeaponAppearance _heroWeaponAppearance;
        private float _currentAttackCooldown = 0f;
        private float _initialCooldown = 2f;
        private bool _canShoot = false;
        private bool _startReloaded;

        public event Action Shot;

        public void TurnOn() =>
            _canShoot = true;

        public void TurnOff() =>
            _canShoot = false;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
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

        private bool IsAvailableAmmo() =>
            _progressService.Progress.WeaponsData.WeaponsAmmoData.IsAmmoAvailable();

        private void Shoot()
        {
            _progressService.Progress.WeaponsData.WeaponsAmmoData.ReduceAmmo();
            _heroWeaponAppearance.ShootTo();
            Shot?.Invoke();
        }
    }
}