using CodeBase.Projectiles;
using CodeBase.Projectiles.Hit;
using CodeBase.Projectiles.Movement;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Services.Constructor
{
    public class ConstructorService : IConstructorService
    {
        private IStaticDataService _staticDataService;

        public ConstructorService(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public void ConstructEnemyProjectile(GameObject projectile, ProjectileTypeId typeId)
        {
            ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(typeId);
            projectile.GetComponent<Projectile>().Construct(typeId);
            projectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
        }

        public void ConstructHeroProjectile(GameObject projectile, ProjectileTypeId projectileTypeId, BlastTypeId blastTypeId)
        {
            ProjectileStaticData projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
            BlastStaticData blastStaticData = _staticDataService.ForBlast(blastTypeId);
            TrailStaticData trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
            projectile.GetComponent<Projectile>().Construct(projectileTypeId);
            projectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
            projectile.GetComponentInChildren<ProjectileBlast>()
                .Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
            projectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
        }

        public void ConstructProjectileLike(GameObject projectile, GameObject newProjectile)
        {
            ProjectileTypeId projectileTypeId = projectile.GetComponent<Projectile>().ProjectileTypeId;
            ProjectileStaticData projectileStaticData = null;
            BlastStaticData blastStaticData = null;
            TrailStaticData trailStaticData = null;

            switch (projectileTypeId)
            {
                case ProjectileTypeId.PistolBullet:
                    projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
                    newProjectile.GetComponent<Projectile>().Construct(projectileTypeId);
                    newProjectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                    break;

                case ProjectileTypeId.Shot:
                    projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
                    newProjectile.GetComponent<Projectile>().Construct(projectileTypeId);
                    newProjectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                    break;

                case ProjectileTypeId.Grenade:
                    projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
                    blastStaticData = _staticDataService.ForBlast(BlastTypeId.Grenade);
                    trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                    newProjectile.GetComponent<Projectile>().Construct(projectileTypeId);
                    newProjectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                    newProjectile.GetComponentInChildren<ProjectileBlast>()
                        .Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                    newProjectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                    break;

                case ProjectileTypeId.RpgRocket:
                    projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
                    blastStaticData = _staticDataService.ForBlast(BlastTypeId.RpgRocket);
                    trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                    newProjectile.GetComponent<Projectile>().Construct(projectileTypeId);
                    newProjectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                    newProjectile.GetComponentInChildren<ProjectileBlast>()
                        .Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                    newProjectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                    break;

                case ProjectileTypeId.RocketLauncherRocket:
                    projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
                    blastStaticData = _staticDataService.ForBlast(BlastTypeId.RocketLauncherRocket);
                    trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                    newProjectile.GetComponent<Projectile>().Construct(projectileTypeId);
                    newProjectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                    newProjectile.GetComponentInChildren<ProjectileBlast>()
                        .Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                    newProjectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                    break;

                case ProjectileTypeId.Bomb:
                    projectileStaticData = _staticDataService.ForProjectile(projectileTypeId);
                    blastStaticData = _staticDataService.ForBlast(BlastTypeId.Bomb);
                    trailStaticData = _staticDataService.ForTrail(projectileStaticData.TrailTypeId);
                    newProjectile.GetComponent<Projectile>().Construct(projectileTypeId);
                    newProjectile.GetComponent<ProjectileMovement>().Construct(projectileStaticData.Speed, projectileStaticData.MovementLifeTime);
                    newProjectile.GetComponentInChildren<ProjectileBlast>()
                        .Construct(blastStaticData.Prefab, blastStaticData.Radius, blastStaticData.Damage);
                    newProjectile.GetComponent<ProjectileTrail>().Construct(trailStaticData);
                    break;
            }
        }
    }
}