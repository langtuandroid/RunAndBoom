using CodeBase.Projectiles.Movement;
using CodeBase.Services;
using CodeBase.Services.Pool;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileTypeId _projectileTypeId;

        private IPoolService _poolService;
        private ProjectileMovement _projectileMovement;

        private void Awake()
        {
            _poolService = AllServices.Container.Single<IPoolService>();
            _projectileMovement = GetComponent<ProjectileMovement>();
            _projectileMovement.Stoped += ReturnToRoot;
        }

        private void ReturnToRoot()
        {
            switch (_projectileTypeId)
            {
                case ProjectileTypeId.PistolBullet:
                    _poolService.ReturnEnemyProjectile(gameObject);
                    break;
                case ProjectileTypeId.Shot:
                    _poolService.ReturnEnemyProjectile(gameObject);
                    break;
                default:
                    _poolService.ReturnHeroProjectile(gameObject);
                    break;
            }
        }
    }
}