using CodeBase.Projectiles.Movement;
using CodeBase.Services;
using CodeBase.Services.Pool;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        // private IObjectsPoolService _objectsPoolService;
        private IHeroProjectilesPoolService _heroProjectilesPool;
        private IEnemyProjectilesPoolService _enemyProjectilesPoolService;
        private ProjectileMovement _projectileMovement;

        public ProjectileTypeId ProjectileTypeId { get; private set; }

        public void Construct(ProjectileTypeId projectileTypeId) =>
            ProjectileTypeId = projectileTypeId;

        private void Awake()
        {
            // _objectsPoolService = AllServices.Container.Single<IObjectsPoolService>();
            _heroProjectilesPool = AllServices.Container.Single<IHeroProjectilesPoolService>();
            _enemyProjectilesPoolService = AllServices.Container.Single<IEnemyProjectilesPoolService>();
            _projectileMovement = GetComponent<ProjectileMovement>();
            _projectileMovement.Stoped += ReturnToRoot;
        }

        private void ReturnToRoot()
        {
            switch (ProjectileTypeId)
            {
                case ProjectileTypeId.PistolBullet:
                    _enemyProjectilesPoolService.ReturnToPool(gameObject);
                    break;

                case ProjectileTypeId.RifleBullet:
                    _enemyProjectilesPoolService.ReturnToPool(gameObject);
                    break;

                case ProjectileTypeId.Shot:
                    _enemyProjectilesPoolService.ReturnToPool(gameObject);
                    break;

                default:
                    _heroProjectilesPool.ReturnToPool(gameObject);
                    break;
            }
        }
    }
}