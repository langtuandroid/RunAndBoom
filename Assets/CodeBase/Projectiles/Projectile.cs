using CodeBase.Projectiles.Movement;
using CodeBase.Services;
using CodeBase.Services.Pool;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private IObjectsPoolService _objectsPoolService;
        private ProjectileMovement _projectileMovement;

        public ProjectileTypeId ProjectileTypeId { get; private set; }

        public void Construct(ProjectileTypeId projectileTypeId)
        {
            ProjectileTypeId = projectileTypeId;
            _projectileMovement = GetComponent<ProjectileMovement>();
            _projectileMovement.Stoped += ReturnToRoot;
        }

        private void Awake() =>
            _objectsPoolService = AllServices.Container.Single<IObjectsPoolService>();

        private void ReturnToRoot()
        {
            _objectsPoolService.ReturnHeroProjectile(ProjectileTypeId.ToString(), gameObject);
            // switch (ProjectileTypeId)
            // {
            //     case ProjectileTypeId.Bullet:
            //         _objectsPoolService.ReturnEnemyProjectile(EnemyWeaponTypeId.Pistol.ToString(),gameObject);
            //         break;
            //
            //     // case ProjectileTypeId.RifleBullet:
            //     //     _objectsPoolService.ReturnEnemyProjectile(gameObject);
            //     //     break;
            //
            //     case ProjectileTypeId.Shot:
            //         _objectsPoolService.ReturnEnemyProjectile(gameObject);
            //         break;
            //
            //     default:
            //         _objectsPoolService.ReturnHeroProjectile(gameObject);
            //         break;
            // }
        }
    }
}