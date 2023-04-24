using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;

namespace CodeBase.Services.Constructor
{
    public interface IConstructorService : IService
    {
        void ConstructEnemyProjectile(GameObject projectile, ProjectileTypeId typeId);

        void ConstructHeroProjectile(GameObject projectile, ProjectileTypeId projectileTypeId, BlastTypeId blastTypeId,
            HeroWeaponTypeId heroWeaponTypeId);

        void ConstructProjectileLike(GameObject projectile, GameObject newProjectile);
    }
}