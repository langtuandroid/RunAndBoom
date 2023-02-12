using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Projectiles;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class WeaponAppearance : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform[] _projectilesRespawns;
        [SerializeField] private Transform[] _muzzlesRespawns;
        [SerializeField] private bool _showProjectiles;

        private List<GameObject> _projectileObjects;
        private List<ProjectileBlast> _blasts;
        private List<ProjectileMovement> _projectileMovements;
        private List<ProjectileTrace> _traces;

        private HeroWeaponSelection _heroWeaponSelection;
        private WeaponStaticData _currentWeaponStaticData;
        private WeaponTypeId _weaponTypeId;
        private ProjectileTraceStaticData _currentProjectileTraceStaticData;
        private GameObject _vfxShot;
        private float _muzzleVfxLifetime = 1f;
        private bool _enemySpotted = false;
        private float _attackCooldownTimer;
        private WaitForSeconds _launchProjectileCooldown;

        private void Awake()
        {
            _projectileObjects = new List<GameObject>(_projectilesRespawns.Length);
            _projectileMovements = new List<ProjectileMovement>(_projectilesRespawns.Length);
            _blasts = new List<ProjectileBlast>(_projectilesRespawns.Length);
            _traces = new List<ProjectileTrace>(_projectilesRespawns.Length);
        }

        public void Construct(HeroWeaponSelection heroWeaponSelection)
        {
            _heroWeaponSelection = heroWeaponSelection;
            _heroWeaponSelection.WeaponSelected += PrepareWeapon;
        }

        private void PrepareWeapon(GameObject weaponPrefab, WeaponStaticData weaponStaticData, ProjectileTraceStaticData projectileTraceStaticData)
        {
            _currentWeaponStaticData = weaponStaticData;
            _currentProjectileTraceStaticData = projectileTraceStaticData;
            _muzzleVfxLifetime = weaponStaticData.MuzzleVfxLifeTime;
            _launchProjectileCooldown = new WaitForSeconds(_currentWeaponStaticData.Cooldown);

            CreateShotVfx();
            CreateProjectiles();
        }

        private void CreateShotVfx()
        {
            _vfxShot = Instantiate(_currentWeaponStaticData.MuzzleVfx, transform.position, transform.rotation);
            _vfxShot.SetActive(false);
        }

        private void CreateProjectiles()
        {
            for (int i = 0; i < _projectilesRespawns.Length; i++)
            {
                GameObject projectileObject = Instantiate(_projectilePrefab, _projectilesRespawns[i].transform.position,
                    _projectilesRespawns[i].transform.rotation, transform);
                _projectileObjects.Add(projectileObject);

                ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
                SetMovementType(ref projectileMovement);
                _projectileMovements.Add(projectileMovement);

                ProjectileTrace projectileTrace = projectileObject.GetComponent<ProjectileTrace>();
                SetTrace(ref projectileTrace);
                _traces.Add(projectileTrace);

                ProjectileBlast projectileBlast = projectileObject.GetComponent<ProjectileBlast>();
                SetBlast(ref projectileBlast);
                // projectileBlast.BlastHappened += ShowLaterProjectile;
                _blasts.Add(projectileBlast);
            }

            ShowProjectile();
        }

        // private void ShowLaterProjectile() =>
        //     StartCoroutine(CoroutineShowLaterProjectile());
        //
        // private IEnumerator CoroutineShowLaterProjectile()
        // {
        //     yield return _launchProjectileCooldown;
        //     ShowProjectile();
        // }

        private void ShowProjectile()
        {
            SetPosition(_projectilesRespawns[0]);
            SetInitialVisibility();
        }

        private void SetInitialVisibility()
        {
            foreach (var projectile in _projectileObjects)
                projectile.SetActive(_showProjectiles);
        }

        private void SetMovementType(ref ProjectileMovement movement)
        {
            switch (_currentWeaponStaticData.WeaponTypeId)
            {
                case WeaponTypeId.GrenadeLauncher:
                    SetGrenadeMovement(ref movement);
                    break;

                case WeaponTypeId.Mortar:
                    SetGrenadeMovement(ref movement);
                    break;

                case WeaponTypeId.RPG:
                    SetBulletMovement(ref movement);
                    break;

                case WeaponTypeId.RocketLaucher:
                    SetBulletMovement(ref movement);
                    break;
            }
        }

        private void SetGrenadeMovement(ref ProjectileMovement movement) =>
            (movement as GrenadeMovement)?.Construct(_currentWeaponStaticData.ProjectileSpeed, transform);

        private void SetBulletMovement(ref ProjectileMovement movement) =>
            (movement as BulletMovement)?.Construct(_currentWeaponStaticData.ProjectileSpeed, transform);

        private void SetTrace(ref ProjectileTrace trace) =>
            trace.Construct(_currentProjectileTraceStaticData);

        private void SetBlast(ref ProjectileBlast blast) =>
            blast.Construct(_currentWeaponStaticData.blastVfxPrefab, _currentWeaponStaticData.BlastRange);

        public void LaunchProjectile() =>
            StartCoroutine(CoroutineLaunchProjectile());

        private IEnumerator CoroutineLaunchProjectile()
        {
            _projectileObjects[0].transform.SetParent(null);
            _projectileMovements[0].Launch();
            Debug.Log("Launched");
            _traces[0].CreateTrace();

            LaunchShotVfx();

            yield return _launchProjectileCooldown;
            ShowProjectile();
        }

        private void SetPosition(Transform projectileTransform)
        {
            _projectileObjects[0].transform.SetParent(transform);
            _projectileObjects[0].transform.position = projectileTransform.position;
            _projectileObjects[0].transform.rotation = projectileTransform.rotation;
        }

        private void LaunchShotVfx()
        {
            SetShotVfx(_muzzlesRespawns[0]);
            StartCoroutine(CoroutineLaunchShotVfx());
        }

        private void SetShotVfx(Transform muzzleTransform)
        {
            _vfxShot.transform.position = muzzleTransform.position;
            _vfxShot.transform.rotation = muzzleTransform.rotation;
        }

        private IEnumerator CoroutineLaunchShotVfx()
        {
            _vfxShot.SetActive(true);
            yield return new WaitForSeconds(_muzzleVfxLifetime);
            _vfxShot.SetActive(false);
        }
    }
}