using CodeBase.Projectiles.Movement;
using CodeBase.Services.Pool;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private IHeroProjectilesPoolService _heroProjectilesPool;
        private IEnemyProjectilesPoolService _enemyProjectilesPoolService;
        private ProjectileMovement _projectileMovement;

        public ProjectileTypeId ProjectileTypeId { get; private set; }

        public void Construct(IHeroProjectilesPoolService heroProjectilesPoolService,
            IEnemyProjectilesPoolService enemyProjectilesPoolService, ProjectileTypeId projectileTypeId)
        {
            _heroProjectilesPool = heroProjectilesPoolService;
            _enemyProjectilesPoolService = enemyProjectilesPoolService;
            ProjectileTypeId = projectileTypeId;
            _projectileMovement = GetComponent<ProjectileMovement>();
            _projectileMovement.Stoped += ReturnToRoot;
        }

        private void ReturnToRoot()
        {
            switch (ProjectileTypeId)
            {
                case ProjectileTypeId.PistolBullet:
                    _enemyProjectilesPoolService.Return(gameObject);
                    break;

                case ProjectileTypeId.RifleBullet:
                    _enemyProjectilesPoolService.Return(gameObject);
                    break;

                case ProjectileTypeId.Shot:
                    _enemyProjectilesPoolService.Return(gameObject);
                    break;

                default:
                    _heroProjectilesPool.Return(gameObject);
                    break;
            }
        }
    }
}