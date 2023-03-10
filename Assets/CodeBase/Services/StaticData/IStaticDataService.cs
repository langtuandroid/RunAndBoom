using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Levels;
using CodeBase.StaticData.ProjectileTraces;
using CodeBase.StaticData.Weapons;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        HeroWeaponStaticData ForHeroWeapon(HeroWeaponTypeId typeId);
        EnemyWeaponStaticData ForEnemyWeapon(EnemyWeaponTypeId typeId);
        ProjectileTraceStaticData ForProjectileTrace(ProjectileTraceTypeId projectileTraceTypeId);

        LevelStaticData ForLevel(string sceneKey);
        // WindowStaticData ForWindow(WindowId windowId);
    }
}