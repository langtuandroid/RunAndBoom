using System.Collections;
using System.Collections.Generic;
using CodeBase.Projectiles;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Weapons
{
    public abstract class BaseWeaponAppearance : MonoBehaviour
    {
        [SerializeField] protected GameObject _projectilePrefab;
        [SerializeField] protected Transform[] _projectilesRespawns;
        [SerializeField] protected Transform[] _muzzlesRespawns;
        [SerializeField] protected bool _showProjectiles;

        protected List<GameObject> ProjectileObjects;
        protected List<ProjectileMovement> ProjectileMovements;
        protected List<ProjectileTrace> ProjectileTraces;
        protected WaitForSeconds LaunchProjectileCooldown;
        private GameObject _vfxShot;
        private float _muzzleVfxLifetime;
        protected int CurrentProjectileIndex = 0;
        private GameObject _muzzleVfx;
        protected float ProjectileSpeed;
        protected float MovementLifeTime;
        private ProjectileTraceStaticData _projectileTraceStaticData;

        protected void Construct(GameObject muzzleVfx, float muzzleVfxLifeTime, float cooldown, float speed, float lifeTime,
            ProjectileTraceStaticData projectileTraceStaticData)
        {
            _muzzleVfx = muzzleVfx;
            _muzzleVfxLifetime = muzzleVfxLifeTime;
            LaunchProjectileCooldown = new WaitForSeconds(cooldown);
            ProjectileSpeed = speed;
            MovementLifeTime = lifeTime;

            ProjectileObjects = new List<GameObject>(_projectilesRespawns.Length);
            ProjectileMovements = new List<ProjectileMovement>(_projectilesRespawns.Length);
            ProjectileTraces = new List<ProjectileTrace>(_projectilesRespawns.Length);
            _projectileTraceStaticData = projectileTraceStaticData;
        }

        protected void CreateShotVfx()
        {
            _vfxShot = Instantiate(_muzzleVfx, transform.position, transform.rotation);
            _vfxShot.SetActive(false);
        }

        protected void SetInitialVisibility()
        {
            foreach (var projectile in ProjectileObjects)
                projectile.SetActive(_showProjectiles);
        }

        protected void SetPosition(int index)
        {
            ProjectileObjects[index].transform.SetParent(transform);
            ProjectileObjects[index].transform.position = _projectilesRespawns[index].position;
            ProjectileObjects[index].transform.rotation = _projectilesRespawns[index].rotation;
        }

        protected void LaunchShotVfx()
        {
            SetShotVfx(_muzzlesRespawns[CurrentProjectileIndex]);
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

        protected void SetBulletMovement(ref ProjectileMovement movement) =>
            (movement as BulletMovement)?.Construct(ProjectileSpeed, transform, MovementLifeTime);

        protected GameObject CreateProjectileObject(int i)
        {
            GameObject projectileObject = Instantiate(_projectilePrefab, _projectilesRespawns[i].transform.position,
                _projectilesRespawns[i].transform.rotation, transform);
            ProjectileObjects.Add(projectileObject);
            return projectileObject;
        }

        private void SetTrace(ref ProjectileTrace trace)
        {
            if (_projectileTraceStaticData.ProjectileTraceTypeId != ProjectileTraceTypeId.None)
                trace.Construct(_projectileTraceStaticData);
        }

        protected void CreateProjectileTrace(GameObject projectileObject)
        {
            ProjectileTrace projectileTrace = projectileObject.GetComponent<ProjectileTrace>();
            SetTrace(ref projectileTrace);
            ProjectileTraces.Add(projectileTrace);
        }

        protected abstract void CreateProjectiles();

        protected abstract void CreateProjectileMovement(GameObject projectileObject);
    }
}