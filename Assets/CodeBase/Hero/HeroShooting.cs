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

        private bool _enableShot = false;
        private GameObject _projectile;

        private void Awake()
        {
        }

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
            _heroAiming.EnemyVisibilityChecked += EnableShoot;
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
        }

        private void CheckWeaponChildrenGameObjects()
        {
            for (int i = 0; i < _weaponTransform.childCount; i++)
            {
                CreateShotVfx();
                CreateProjectile();
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

        private void EnableShoot() =>
            _enableShot = true;

        private void Shoot()
        {
            Debug.Log($"enableShot {_enableShot}");

            if (_enableShot)
                StartCoroutine(CoroutineShoot());
        }

        private IEnumerator CoroutineShoot()
        {
            _enableShot = false;
            SetProjectile(_projectileRespawns[0]);
            LaunchProjectile();
            LaunchShotVfx();
            Debug.Log($"Cooldown {_currentWeaponStaticData.Cooldown}");
            float start = Time.deltaTime;
            Debug.Log($"start {start}");

            WaitForSeconds wait = new WaitForSeconds(_currentWeaponStaticData.Cooldown);
            yield return wait;

            float end = Time.deltaTime;
            Debug.Log($"end {end}");
            float result = end - start;
            Debug.Log($"result {result}");
        }

        private void LaunchProjectile()
        {
            _projectile.SetActive(true);
            _projectile.GetComponent<Rigidbody>().velocity = _muzzlesRespawns[0].transform.forward * _currentWeaponStaticData.ProjectileSpeed;
        }

        private void SetProjectile(Transform projectileTransform)
        {
            _projectile.transform.position = projectileTransform.position;
            _projectile.transform.rotation = projectileTransform.rotation;
        }

        private void CreateProjectile()
        {
            _projectile = Instantiate(_currentWeaponStaticData.ProjectilePrefab, _weaponTransform.position, _weaponTransform.rotation);
            _projectile.SetActive(false);
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