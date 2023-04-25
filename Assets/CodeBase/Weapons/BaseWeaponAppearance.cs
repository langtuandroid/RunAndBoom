using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using CodeBase.Projectiles;
using CodeBase.Projectiles.Hit;
using CodeBase.Projectiles.Movement;
using CodeBase.Services;
using CodeBase.Services.Pool;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Weapons
{
    public abstract class BaseWeaponAppearance : MonoBehaviour
    {
        [FormerlySerializedAs("_projectilesRespawns")] [SerializeField]
        public Transform[] ProjectilesRespawns;

        [FormerlySerializedAs("MuzzlesRespawns")] [FormerlySerializedAs("_muzzlesRespawns")] [SerializeField]
        protected Transform[] ShotVfxsRespawns;

        [FormerlySerializedAs("ShowProjectiles")] [SerializeField]
        protected bool _showProjectiles;

        [SerializeField] protected ShotVfxsContainer ShotVfxsContainer;

        protected IObjectsPoolService PoolService;
        private bool _initialVisibility;
        [SerializeField] private List<GameObject> _projectiles;
        private ProjectileTypeId? _projectileTypeId;
        private bool _filled;
        private GameObject _firstProjectile;
        private IDeath _death;
        private bool _enabled = true;

        protected WaitForSeconds LaunchProjectileCooldown { get; private set; }

        private void OnEnable() =>
            Enable();

        private void OnDisable() =>
            Disable();

        protected void Construct(IDeath death, float shotVfxLifeTime, float cooldown, ProjectileTypeId projectileTypeId,
            ShotVfxTypeId shotVfxTypeId)
        {
            _death = death;
            PoolService = AllServices.Container.Single<IObjectsPoolService>();
            ShotVfxsContainer.Construct(shotVfxLifeTime, shotVfxTypeId, transform);
            LaunchProjectileCooldown = new WaitForSeconds(cooldown);
            _projectiles = new List<GameObject>(ProjectilesRespawns.Length);
            _projectileTypeId = projectileTypeId;
            Enable();

            _death.Died += Disable;
        }

        private void Disable()
        {
            _enabled = false;

            if (_death != null)
                _death.Died -= Disable;
        }

        private void Enable()
        {
            _enabled = true;

            if (_death != null)
                _death.Died += Disable;
        }

        protected void ReadyToShoot()
        {
            if (gameObject.activeInHierarchy && (_filled == false || _projectiles.Count == 0) && _enabled)
            {
                foreach (Transform respawn in ProjectilesRespawns)
                {
                    var projectile = SetNewProjectile(respawn);
                    projectile.SetActive(true);
                    projectile.GetComponentInChildren<MeshRenderer>().enabled = _showProjectiles;
                    projectile.GetComponentInChildren<ProjectileBlast>()?.OffCollider();
                    _projectiles.Add(projectile);
                }

                _filled = true;
            }
        }

        protected void Launch()
        {
            GameObject projectile = GetFirstProjectile();
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            TuneProjectileBeforeLaunch(projectile, projectileMovement);
        }

        protected void Launch(Vector3 targetPosition)
        {
            GameObject projectile = GetFirstProjectile();
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            (projectileMovement as BombMovement)?.SetTargetPosition(targetPosition);
            TuneProjectileBeforeLaunch(projectile, projectileMovement);
        }

        private void TuneProjectileBeforeLaunch(GameObject projectile, ProjectileMovement projectileMovement)
        {
            projectile.GetComponentInChildren<MeshRenderer>().enabled = true;
            projectile.GetComponentInChildren<ProjectileBlast>()?.OnCollider();
            projectile.SetActive(true);
            projectileMovement.Launch();
            projectile.transform.SetParent(null);
            ShowTrail(projectile);
        }

        private GameObject GetFirstProjectile()
        {
            _firstProjectile = _projectiles.First();
            return _firstProjectile;
        }

        protected ProjectileMovement GetMovement()
        {
            _firstProjectile = _projectiles.First();
            return _firstProjectile.GetComponent<ProjectileMovement>();
        }

        private void ShowTrail(GameObject projectile)
        {
            if (_projectileTypeId != null)
                projectile.GetComponent<ProjectileTrail>().ShowTrail();
        }

        private GameObject SetNewProjectile(Transform respawn)
        {
            GameObject projectile = GetProjectile();

            projectile.transform.SetParent(respawn);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.rotation = respawn.rotation;
            return projectile;
        }

        protected void Release()
        {
            _projectiles.Remove(_firstProjectile);
            _firstProjectile = null;

            if (_projectiles.Count == 0)
                _filled = false;
        }

        protected abstract GameObject GetProjectile();
    }
}