using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Weapon;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        WeaponStaticData ForWeaponUI(WeaponTypeId typeId);

        LevelStaticData ForLevel(string sceneKey);
        // WindowStaticData ForWindow(WindowId windowId);
    }
}