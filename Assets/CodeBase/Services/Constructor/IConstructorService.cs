using CodeBase.Services.Pool;
using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Services.Constructor
{
    public interface IConstructorService : IService
    {
        void ConstructEnemyProjectile(IHeroProjectilesPoolService heroProjectilesPoolService,
            IEnemyProjectilesPoolService enemyProjectilesPoolService, GameObject projectile, float damage,
            ProjectileTypeId typeId);

        void ConstructHeroProjectile(IHeroProjectilesPoolService heroProjectilesPoolService,
            IEnemyProjectilesPoolService enemyProjectilesPoolService, GameObject projectile,
            ProjectileTypeId projectileTypeId, BlastTypeId blastTypeId,
            HeroWeaponTypeId heroWeaponTypeId);

        void ConstructProjectileLike(GameObject projectile, GameObject newProjectile);
    }
}