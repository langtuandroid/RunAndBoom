using CodeBase.StaticData.Level;
using CodeBase.StaticData.Monster;
using CodeBase.StaticData.Weapon;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        WeaponStaticData ForWeaponUI(WeaponTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowStaticData ForWindow(WindowId windowId);
    }
}