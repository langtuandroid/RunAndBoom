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
            switch (ProjectileTypeId)
            {
                case ProjectileTypeId.PistolBullet:
                case ProjectileTypeId.RifleBullet:
                case ProjectileTypeId.Shot:
                    _objectsPoolService.ReturnEnemyProjectile(ProjectileTypeId.ToString(), gameObject);
                    break;

                case ProjectileTypeId.None:
                case ProjectileTypeId.Grenade:
                case ProjectileTypeId.RocketLauncherRocket:
                case ProjectileTypeId.RpgRocket:
                case ProjectileTypeId.Bomb:
                default:
                    _objectsPoolService.ReturnHeroProjectile(ProjectileTypeId.ToString(), gameObject);
                    break;
            }
        }
    }
}