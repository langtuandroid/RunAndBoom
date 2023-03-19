using System.Collections;
using System.Collections.Generic;
using CodeBase.Enemy.Attacks;
using CodeBase.Projectiles;
using CodeBase.Projectiles.Movement;
using CodeBase.StaticData.ProjectileTraces;
using UnityEngine;

namespace CodeBase.Weapons
{
    public abstract class BaseWeaponAppearance : MonoBehaviour
    {
        [SerializeField] protected GameObject _projectilePrefab;
        [SerializeField] protected Transform[] _projectilesRespawns;
        [SerializeField] protected Transform[] _muzzlesRespawns;
        [SerializeField] protected bool _showProjectiles;

        private GameObject _vfxShot;
        private float _muzzleVfxLifetime;
        protected int CurrentProjectileIndex = 0;
        private GameObject _muzzleVfx;
        private ProjectileTraceStaticData _projectileTraceStaticData;
        protected bool CanShoot = true;

        protected List<GameObject> ProjectileObjects { get; private set; }
        protected List<ProjectileMovement> ProjectileMovements { get; private set; }
        protected List<ProjectileTrace> ProjectileTraces { get; private set; }
        protected WaitForSeconds LaunchProjectileCooldown { get; private set; }
        protected float ProjectileSpeed { get; private set; }
        protected float MovementLifeTime { get; private set; }
        protected float Damage { get; private set; }
        protected Attack Attack { get; private set; }

        private void Awake() =>
            Attack = GetComponent<Attack>();

        protected void Construct(GameObject muzzleVfx, float muzzleVfxLifeTime, float cooldown, float speed, float lifeTime, float damage,
            ProjectileTraceStaticData projectileTraceStaticData)
        {
            _muzzleVfx = muzzleVfx;
            _muzzleVfxLifetime = muzzleVfxLifeTime;
            LaunchProjectileCooldown = new WaitForSeconds(cooldown);
            ProjectileSpeed = speed;
            MovementLifeTime = lifeTime;
            Damage = damage;

            ProjectileObjects = new List<GameObject>(_projectilesRespawns.Length * 3);
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

        protected void SetPosition(int index, Transform transform)
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

        protected void ChangeProjectileIndex()
        {
            bool notLastIndex = CurrentProjectileIndex < (ProjectileObjects.Count - 1);

            if (notLastIndex)
                CurrentProjectileIndex++;
            else
                CurrentProjectileIndex = 0;
        }

        protected abstract void CreateProjectiles();

        protected abstract void CreateProjectileMovement(GameObject projectileObject);
    }
}