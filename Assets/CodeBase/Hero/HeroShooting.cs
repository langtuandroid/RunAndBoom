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
        [SerializeField] private EnemiesChecker _enemiesChecker;

        private IPlayerProgressService _progressService;
        private HeroWeaponAppearance _heroWeaponAppearance;
        private bool _enemySpotted = false;
        private Vector3 _enemyPosition;
        private bool _canShoot = true;

        public event Action Shot;

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
            TrailStaticData trailStaticData)
        {
            _heroWeaponAppearance = weaponPrefab.GetComponent<HeroWeaponAppearance>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                TryShoot();
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
            if (_enemySpotted && _canShoot && IsAvailableAmmo())
                Shoot();
        }

        private bool IsAvailableAmmo() =>
            _progressService.Progress.WeaponsData.WeaponsAmmoData.IsAmmoAvailable();

        private void Shoot()
        {
            _progressService.Progress.WeaponsData.WeaponsAmmoData.ReduceAmmo();
            _heroWeaponAppearance.ShootTo(_enemyPosition);
            Shot?.Invoke();
        }
    }
}