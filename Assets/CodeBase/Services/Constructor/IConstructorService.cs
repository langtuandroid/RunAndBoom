using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Services.Constructor
{
    public interface IConstructorService : IService
    {
        void ConstructEnemyProjectile(GameObject projectile, ProjectileTypeId typeId);
        void ConstructHeroProjectile(GameObject projectile, ProjectileTypeId projectileTypeId, BlastTypeId blastTypeId);
        void ConstructProjectileLike(GameObject projectile, GameObject newProjectile);
    }
}