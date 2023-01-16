using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Monster;
using CodeBase.StaticData.Weapon;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataMonstersPath = "StaticData/Monsters";
        private const string StaticDataWeaponsPath = "StaticData/Weapons";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/Windows";

        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<WeaponTypeId, WeaponStaticData> _weapons;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowStaticData> _windows;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(StaticDataMonstersPath)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _weapons = Resources
                .LoadAll<WeaponStaticData>(StaticDataWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windows = Resources
                .LoadAll<WindowStaticData>(StaticDataWindowsPath)
                .ToDictionary(x => x.WindowId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public WeaponStaticData ForWeaponUI(WeaponTypeId typeId) =>
            _weapons.TryGetValue(typeId, out WeaponStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public WindowStaticData ForWindow(WindowId windowId) =>
            _windows.TryGetValue(windowId, out WindowStaticData windowData)
                ? windowData
                : null;
    }
}