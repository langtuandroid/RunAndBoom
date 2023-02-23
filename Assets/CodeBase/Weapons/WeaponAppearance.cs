using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Projectiles;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class WeaponAppearance : BaseWeaponAppearance
    {
        private List<ProjectileBlast> _blasts;

        private HeroWeaponSelection _heroWeaponSelection;
        private WeaponStaticData _currentWeaponStaticData;
        private WeaponTypeId _weaponTypeId;
        private bool _enemySpotted = false;
        private int _currentProjectileIndex = 0;

        private void Awake()
        {
        }

        public void Construct(HeroWeaponSelection heroWeaponSelection)
        {
            _heroWeaponSelection = heroWeaponSelection;
            _heroWeaponSelection.WeaponSelected += PrepareWeapon;

            base.Construct();
            _blasts = new List<ProjectileBlast>(_projectilesRespawns.Length);
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

                ProjectileBlast projectileBlast = projectileObject.GetComponentInChildren<ProjectileBlast>();
                SetBlast(ref projectileBlast);
                _blasts.Add(projectileBlast);
            }

            SetPosition(_currentProjectileIndex);
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
                    SetBombMovement(ref movement);
                    break;

                case WeaponTypeId.RPG:
                    SetBulletMovement(ref movement);
                    break;

                case WeaponTypeId.RocketLauncher:
                    SetBulletMovement(ref movement);
                    break;
            }
        }

        private void SetGrenadeMovement(ref ProjectileMovement movement) =>
            (movement as GrenadeMovement)?.Construct(_currentWeaponStaticData.ProjectileSpeed, transform, _currentWeaponStaticData.MovementLifeTime);

        private void SetBombMovement(ref ProjectileMovement movement) =>
            (movement as BombMovement)?.Construct(_currentWeaponStaticData.ProjectileSpeed, transform, _currentWeaponStaticData.MovementLifeTime);

        private void SetBulletMovement(ref ProjectileMovement movement) =>
            (movement as BulletMovement)?.Construct(_currentWeaponStaticData.ProjectileSpeed, transform, _currentWeaponStaticData.MovementLifeTime);

        private void SetTrace(ref ProjectileTrace trace) =>
            trace.Construct(_currentProjectileTraceStaticData);

        private void SetBlast(ref ProjectileBlast blast) =>
            blast.Construct(_currentWeaponStaticData.blastVfxPrefab, _currentWeaponStaticData.BlastRange);

        public void LaunchProjectile(Vector3 enemyPosition) =>
            StartCoroutine(CoroutineLaunchProjectile(enemyPosition));

        private IEnumerator CoroutineLaunchProjectile(Vector3 enemyPosition)
        {
            _projectileObjects[_currentProjectileIndex].transform.SetParent(null);
            _projectileObjects[_currentProjectileIndex].SetActive(true);

            (_projectileMovements[_currentProjectileIndex] as BombMovement)?.SetTargetPosition(enemyPosition);
            _projectileMovements[_currentProjectileIndex].Launch();
            Debug.Log("Launched");
            Debug.Log($"index {_currentProjectileIndex}");
            _traces[_currentProjectileIndex].CreateTrace();

            LaunchShotVfx();

            yield return _launchProjectileCooldown;
            ChangeProjectileIndex();
            SetPosition(_currentProjectileIndex);
        }

        private void ChangeProjectileIndex()
        {
            bool notLastIndex = _currentProjectileIndex < (_projectileObjects.Count - 1);

            if (notLastIndex)
            {
                _currentProjectileIndex++;
            }
            else
            {
                _currentProjectileIndex = 0;
                SetInitialVisibility();
            }
        }

        private void SetPosition(int index)
        {
            _projectileObjects[index].transform.SetParent(transform);
            _projectileObjects[index].transform.position = _projectilesRespawns[index].position;
            _projectileObjects[index].transform.rotation = _projectilesRespawns[index].rotation;
        }

        private void LaunchShotVfx()
        {
            SetShotVfx(_muzzlesRespawns[_currentProjectileIndex]);
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