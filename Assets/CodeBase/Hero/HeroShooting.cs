using System.Collections;
using System.Collections.Generic;
using CodeBase.Services.Input.Platforms;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroShooting : MonoBehaviour
    {
        private const string MuzzleName = "Muzzle";
        private const string ProjectileRespawnName = "ProjectileRespawn";

        private IStaticDataService _staticDataService;
        private IPlatformInputService _platformInputService;

        private HeroWeaponSelection _heroWeaponSelection;
        private HeroAiming _heroAiming;
        private WeaponStaticData _currentWeaponStaticData;
        private ProjectileTraceStaticData _currentProjectileTraceStaticData;
        private List<Transform> _muzzlesRespawns = new List<Transform>();
        private List<Transform> _projectileRespawns = new List<Transform>();

        private Transform _weaponTransform;
        private GameObject _vfxShot;
        private float _vfxShotLifetime = 1f;

        private bool _enableSpotted = false;
        private float _currentAttackCooldown;
        private GameObject _projectilePrefab;
        private Projectile.Projectile _projectile;

        private void Update() =>
            UpdateCooldown();

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private bool CanAttack() =>
            CooldownUp();

        [Inject]
        public void Construct(IStaticDataService staticDataService, IPlatformInputService platformInputService)
        {
            _staticDataService = staticDataService;
            _platformInputService = platformInputService;

            SubscribeServicesEvents();
        }


        private void SubscribeServicesEvents()
        {
            _heroWeaponSelection = GetComponent<HeroWeaponSelection>();
            _heroAiming = GetComponent<HeroAiming>();
            _heroWeaponSelection.WeaponSelected += PrepareWeaponVfx;
            _heroAiming.EnemyVisibilityChecked += EnemySpotted;
            _platformInputService.Shot += Shoot;
        }

        // private void OnDisable()
        // {
        //     _heroWeaponSelection.WeaponSelected -= PrepareWeaponVfx;
        //     _enemiesChecker.EnemyVisibilityChecked -= EnableShoot;
        // }

        private void PrepareWeaponVfx(WeaponStaticData weaponStaticData, Transform weaponTransform)
        {
            _currentWeaponStaticData = weaponStaticData;
            _currentProjectileTraceStaticData = _staticDataService.ForProjectileTrace(_currentWeaponStaticData.ProjectileTraceTypeId);
            _weaponTransform = weaponTransform;

            CheckWeaponChildrenGameObjects();
            CreateShotVfx();
            CreateProjectile();
        }

        private void CheckWeaponChildrenGameObjects()
        {
            for (int i = 0; i < _weaponTransform.childCount; i++)
            {
                AddMuzzlesPositions(i);
                ProjectileRespawnPositions(i);
            }
        }

        private void CreateShotVfx()
        {
            _vfxShot = Instantiate(_currentWeaponStaticData.MuzzleVfx, _weaponTransform.position, _weaponTransform.rotation);
            _vfxShot.SetActive(false);
        }

        private void ProjectileRespawnPositions(int i)
        {
            if (_weaponTransform.GetChild(i).gameObject.name.Contains(ProjectileRespawnName))
                _projectileRespawns.Add(_weaponTransform.GetChild(i).gameObject.transform);
        }

        private void AddMuzzlesPositions(int i)
        {
            if (_weaponTransform.GetChild(i).gameObject.name.Contains(MuzzleName))
                _muzzlesRespawns.Add(_weaponTransform.GetChild(i).gameObject.transform);
        }

        private void EnemySpotted() =>
            _enableSpotted = true;

        private void Shoot()
        {
            if (_enableSpotted && CanAttack())
                StartCoroutine(CoroutineShoot());
        }

        private IEnumerator CoroutineShoot()
        {
            Debug.Log("CoroutineShoot");
            SetProjectile(_projectileRespawns[0]);
            LaunchProjectile();
            LaunchShotVfx();
            _currentAttackCooldown = _currentWeaponStaticData.Cooldown;
            yield return null;
        }

        private void LaunchProjectile()
        {
            _projectilePrefab.SetActive(true);
            _projectile.CreateTrace();
        }

        private void SetProjectile(Transform projectileTransform)
        {
            _projectilePrefab.transform.position = projectileTransform.position;
            _projectilePrefab.transform.rotation = projectileTransform.rotation;
        }

        private void CreateProjectile()
        {
            _projectilePrefab = Instantiate(_currentWeaponStaticData.ProjectilePrefab, _weaponTransform.position, _weaponTransform.rotation);
            _projectilePrefab.SetActive(false);
            _projectile = _projectilePrefab.GetComponent<Projectile.Projectile>();
            _projectile.Construct(_currentWeaponStaticData.BlastVfx, _currentProjectileTraceStaticData);
            // _projectile.SetSpeed(_muzzlesRespawns[0].transform.forward * 0f);
            _projectile.SetSpeed(_muzzlesRespawns[0].transform.forward * _currentWeaponStaticData.ProjectileSpeed);
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