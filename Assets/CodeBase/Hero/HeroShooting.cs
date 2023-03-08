using CodeBase.Services;
using CodeBase.Services.Input.Platforms;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        [SerializeField] private HeroWeaponSelection _heroWeaponSelection;
        [SerializeField] private EnemiesChecker _enemiesChecker;

        // private IPlatformInputService _platformInputService;
        private IPlayerProgressService _progressService;
        private HeroWeaponAppearance _heroWeaponAppearance;
        private bool _enemySpotted = false;
        private float _currentAttackCooldown = 0f;
        private float _weaponCooldown = 0f;
        private Vector3 _enemyPosition;

        private void Awake()
        {
            // _platformInputService = AllServices.Container.Single<IPlatformInputService>();
            _progressService = AllServices.Container.Single<IPlayerProgressService>();

            _heroWeaponSelection.WeaponSelected += GetCurrentWeaponObject;
            _enemiesChecker.FoundClosestEnemy += EnemySpotted;
            _enemiesChecker.EnemyNotFound += EnemyNotSpotted;
            // _platformInputService.Shot += TryShoot;
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

            if (Input.GetMouseButton(0))
                TryShoot();
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void EnemyNotSpotted() =>
            _enemySpotted = false;

        private void OnDisable()
        {
            _enemiesChecker.FoundClosestEnemy -= EnemySpotted;
            _enemiesChecker.EnemyNotFound -= EnemyNotSpotted;
            // _platformInputService.Shot -= TryShoot;
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
            _progressService.Progress.WeaponsData.IsAmmoAvailable();

        private void Shoot()
        {
            _progressService.Progress.WeaponsData.ReduceAmmo();
            _currentAttackCooldown = _weaponCooldown;
            _heroWeaponAppearance.ShootTo(_enemyPosition);
        }
    }
}