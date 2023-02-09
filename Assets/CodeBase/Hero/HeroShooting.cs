using System.Collections;
using System.Collections.Generic;
using CodeBase.Projectile;
using CodeBase.Services.Input.Platforms;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        private IStaticDataService _staticDataService;
        private IPlatformInputService _platformInputService;

        private HeroWeaponSelection _heroWeaponSelection;
        private EnemiesChecker _enemiesChecker;
        private WeaponStaticData _currentWeaponStaticData;
        private WeaponTypeId _weaponTypeId;
        private ProjectileTraceStaticData _currentProjectileTraceStaticData;
        private List<Transform> _muzzlesRespawns = new List<Transform>();
        private List<Transform> _projectileRespawns = new List<Transform>();
        private Transform _weaponTransform;
        private GameObject _vfxShot;
        private float _vfxShotLifetime = 1f;

        private bool _enemySpotted = false;
        private float _currentAttackCooldown;
        private GameObject _projectilePrefab;
        private Projectile.Projectile _projectile;

        private void Awake()
        {
            _heroWeaponSelection = GetComponent<HeroWeaponSelection>();
            _enemiesChecker = GetComponent<EnemiesChecker>();
            _heroWeaponSelection.WeaponSelected += PrepareWeaponVfx;
            _enemiesChecker.EnemyVisibilityChecked += EnemySpotted;
            _enemiesChecker.EnemyNotFound += EnemyNotSpotted;
            _platformInputService.Shot += TryShoot;
        }

        private void Update() =>
            UpdateCooldown();

        [Inject]
        public void Construct(IStaticDataService staticDataService, IPlatformInputService platformInputService)
        {
            _staticDataService = staticDataService;
            _platformInputService = platformInputService;
        }

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private void EnemyNotSpotted() =>
            _enemySpotted = false;

        private void OnDisable()
        {
            _heroWeaponSelection.WeaponSelected -= PrepareWeaponVfx;
            _enemiesChecker.EnemyVisibilityChecked -= EnemySpotted;
            _enemiesChecker.EnemyNotFound -= EnemyNotSpotted;
            _platformInputService.Shot -= TryShoot;
        }

        private void PrepareWeaponVfx(WeaponStaticData weaponStaticData, Transform weaponTransform)
        {
            _currentWeaponStaticData = weaponStaticData;
            _weaponTypeId = _currentWeaponStaticData.WeaponTypeId;
            _currentProjectileTraceStaticData = _staticDataService.ForProjectileTrace(_currentWeaponStaticData.ProjectileTraceTypeId);
            _weaponTransform = weaponTransform;

            AddMuzzlesPositions();
            ProjectileRespawnPositions();
            CreateShotVfx();
            CreateProjectile();
        }

        private void CreateShotVfx()
        {
            _vfxShot = Instantiate(_currentWeaponStaticData.MuzzleVfx, _weaponTransform.position, _weaponTransform.rotation);
            _vfxShot.SetActive(false);
        }

        private void ProjectileRespawnPositions()
        {
            foreach (Transform projectilesRespawn in _weaponTransform.GetComponent<Weapon>().ProjectilesRespawns)
                _projectileRespawns.Add(projectilesRespawn);
        }

        private void AddMuzzlesPositions()
        {
            foreach (Transform muzzlesRespawn in _weaponTransform.GetComponent<Weapon>().MuzzlesRespawns)
                _muzzlesRespawns.Add(muzzlesRespawn);
        }

        private void EnemySpotted() =>
            _enemySpotted = true;

        private void TryShoot()
        {
            if (_enemySpotted && CanAttack())
                Shoot();
        }

        private bool CanAttack() =>
            CooldownUp();

        private void Shoot()
        {
            Debug.Log("Shoot");
            _currentAttackCooldown = _currentWeaponStaticData.Cooldown;
            SetProjectile(_projectileRespawns[0]);
            StartCoroutine(LaunchProjectile());
            LaunchShotVfx();
        }

        private IEnumerator LaunchProjectile()
        {
            if (_weaponTypeId == WeaponTypeId.GrenadeLauncher)
                _projectilePrefab.SetActive(true);

            _projectile.Launch();
            _projectile.CreateTrace();

            yield return new WaitForSeconds(_currentAttackCooldown);

            _projectilePrefab.SetActive(false);
        }

        private void SetProjectile(Transform projectileTransform)
        {
            float y = 0f;

            if (_weaponTypeId == WeaponTypeId.RPG)
                y = 0.14f;

            _projectilePrefab.transform.position = projectileTransform.position + new Vector3(0, y, 0);
            _projectilePrefab.transform.rotation = projectileTransform.rotation;
        }

        private void CreateProjectile()
        {
            _projectilePrefab = Instantiate(_currentWeaponStaticData.ProjectilePrefab, _weaponTransform.position, _weaponTransform.rotation);

            SetProjectileObjectVisibility();

            switch (_currentWeaponStaticData.WeaponTypeId)
            {
                case WeaponTypeId.GrenadeLauncher:
                    SetGrenadeBehavior();
                    break;

                case WeaponTypeId.Mortar:
                    SetGrenadeBehavior();
                    break;

                case WeaponTypeId.RPG:
                    SetBulletBehavior();
                    break;

                case WeaponTypeId.RocketLaucher:
                    SetBulletBehavior();
                    break;
            }
        }

        private void SetGrenadeBehavior()
        {
            _projectile = _projectilePrefab.GetComponent<Grenade>();
            (_projectile as Grenade)?.Construct(_currentWeaponStaticData.BlastVfx, _currentProjectileTraceStaticData,
                _currentWeaponStaticData.ProjectileSpeed,
                _currentWeaponStaticData.BlastRange);
        }

        private void SetBulletBehavior()
        {
            _projectile = _projectilePrefab.GetComponent<Bullet>();
            (_projectile as Bullet)?.Construct(_currentWeaponStaticData.BlastVfx, _currentProjectileTraceStaticData,
                _currentWeaponStaticData.ProjectileSpeed,
                _currentWeaponStaticData.BlastRange, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        }

        private void SetProjectileObjectVisibility()
        {
            if (_weaponTypeId == WeaponTypeId.RPG || _weaponTypeId == WeaponTypeId.Mortar || _weaponTypeId == WeaponTypeId.RocketLaucher)
                _projectilePrefab.SetActive(true);
            else
                _projectilePrefab.SetActive(false);
        }

        private void LaunchShotVfx()
        {
            foreach (Transform muzzleRespawn in _muzzlesRespawns)
            {
                SetShotVfx(muzzleRespawn);
                StartCoroutine(CoroutineLaunchShotVfx());
            }
        }

        private void SetShotVfx(Transform muzzleTransform)
        {
            _vfxShot.transform.position = muzzleTransform.position;
            _vfxShot.transform.rotation = muzzleTransform.rotation;
        }

        private IEnumerator CoroutineLaunchShotVfx()
        {
            _vfxShot.SetActive(true);
            yield return new WaitForSeconds(_vfxShotLifetime);
            _vfxShot.SetActive(false);
        }
    }
}