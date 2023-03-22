using System;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;
        [SerializeField] private EnemiesChecker _enemiesChecker;

        private IPlayerProgressService _progressService;
        private HeroWeaponAppearance _heroWeaponAppearance;
        private bool _enemySpotted = false;
        private float _currentAttackCooldown = 0f;
        private float _weaponCooldown = 0f;
        private Vector3 _enemyPosition;
        private bool _canShoot = true;

        public Action<float> OnStartReloading;
        public Action OnStopReloading;

        public void TurnOn() =>
            _canShoot = true;

        public void TurnOff() =>
            _canShoot = false;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();

            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            _enemiesChecker.FoundClosestEnemy += EnemySpotted;
            _enemiesChecker.EnemyNotFound += EnemyNotSpotted;
        }

        private void GetCurrentWeaponObject(GameObject weaponPrefab, HeroWeaponStaticData heroWeaponStaticData,
            ProjectileTraceStaticData projectileTraceStaticData)
        {
            _heroWeaponAppearance = weaponPrefab.GetComponent<HeroWeaponAppearance>();
            _weaponCooldown = heroWeaponStaticData.Cooldown;
        }

        private void Update()
        {
            UpdateCooldown();

            if (_canShoot)
                if (Input.GetMouseButton(0))
                    TryShoot();
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
            {
                OnStartReloading?.Invoke(_currentAttackCooldown / _weaponCooldown);
                _currentAttackCooldown -= Time.deltaTime;
            }
            else
            {
                OnStopReloading?.Invoke();
            }
        }

        private void EnemyNotSpotted() =>
            _enemySpotted = false;

        private void OnDisable()
        {
            _enemiesChecker.FoundClosestEnemy -= EnemySpotted;
            _enemiesChecker.EnemyNotFound -= EnemyNotSpotted;
        }

        private void EnemySpotted(GameObject enemy)
        {
            _enemyPosition = enemy.gameObject.transform.position;
            _enemySpotted = true;
        }

        private void TryShoot()
        {
            if (_enemySpotted && CooldownUp() && IsAvailableAmmo())
                Shoot();
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private bool IsAvailableAmmo() =>
            _progressService.Progress.WeaponsData.WeaponsAmmoData.IsAmmoAvailable();

        private void Shoot()
        {
            _progressService.Progress.WeaponsData.WeaponsAmmoData.ReduceAmmo();
            _currentAttackCooldown = _weaponCooldown;
            _heroWeaponAppearance.ShootTo(_enemyPosition);
        }
    }
}