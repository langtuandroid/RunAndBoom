using System.Collections.Generic;
using System.Linq;
using CodeBase.Projectiles;
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
        protected Transform[] ProjectilesRespawns;

        [FormerlySerializedAs("MuzzlesRespawns")] [FormerlySerializedAs("_muzzlesRespawns")] [SerializeField]
        protected Transform[] ShotVfxsRespawns;

        [FormerlySerializedAs("ShowProjectiles")] [SerializeField]
        protected bool _showProjectiles;

        [SerializeField] protected ShotVfxsContainer ShotVfxsContainer;

        protected IPoolService PoolService;
        private bool _initialVisibility;
        [SerializeField] private List<GameObject> _projectiles;
        private ProjectileTypeId? _projectileTypeId;

        protected WaitForSeconds LaunchProjectileCooldown { get; private set; }

        protected void Construct(float shotVfxLifeTime, float cooldown, ProjectileTypeId projectileTypeId, ShotVfxTypeId shotVfxTypeId)
        {
            PoolService = AllServices.Container.Single<IPoolService>();
            ShotVfxsContainer.Construct(shotVfxLifeTime, shotVfxTypeId, transform);
            LaunchProjectileCooldown = new WaitForSeconds(cooldown);
            _projectiles = new List<GameObject>(ProjectilesRespawns.Length);
            _projectileTypeId = projectileTypeId;
        }

        protected void ReadyToShoot()
        {
            foreach (Transform respawn in ProjectilesRespawns)
            {
                var projectile = SetNewProjectile(respawn);
                _projectiles.Add(projectile);

                projectile.SetActive(true);
                // projectile.SetActive(_showProjectiles);
            }
        }

        protected void Launch()
        {
            GameObject projectile = _projectiles.First();
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
            projectileMovement.gameObject.SetActive(true);
            projectileMovement.gameObject.transform.SetParent(null);
            projectileMovement.Launch();

            ShowTrail(projectile);

            _projectiles.Remove(projectile);
        }

        protected ProjectileMovement GetMovement() =>
            _projectiles.First().GetComponent<ProjectileMovement>();

        private void ShowTrail(GameObject projectile)
        {
            if (_projectileTypeId != null)
                projectile.GetComponent<ProjectileTrail>().ShowTrail();
        }

        private GameObject SetNewProjectile(Transform respawn)
        {
            GameObject projectile = GetProjectile();

            projectile.transform.SetParent(respawn);
            projectile.transform.position = respawn.position;
            projectile.transform.rotation = respawn.rotation;
            return projectile;
        }

        protected abstract GameObject GetProjectile();
    }
}