using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataWeaponsPath = "StaticData/Weapons";
        private const string StaticDataLevelsPath = "StaticData/Levels";

        private const string StaticDataProjectileTracesPath = "StaticData/ProjectileTraces";
        // private const string StaticDataWindowsPath = "StaticData/Windows";

        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<WeaponTypeId, WeaponStaticData> _weapons;
        private Dictionary<string, LevelStaticData> _levels;

        private Dictionary<ProjectileTraceTypeId, ProjectileTraceStaticData> _projectileTraces;
        // private Dictionary<WindowId, WindowStaticData> _windows;

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.enemyTypeId, x => x);

            _weapons = Resources
                .LoadAll<WeaponStaticData>(StaticDataWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _projectileTraces = Resources
                .LoadAll<ProjectileTraceStaticData>(StaticDataProjectileTracesPath)
                .ToDictionary(x => x.ProjectileTraceTypeId, x => x);

            // _windows = Resources
            //     .LoadAll<WindowStaticData>(StaticDataWindowsPath)
            //     .ToDictionary(x => x.WindowId, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData staticData)
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

        public ProjectileTraceStaticData ForProjectileTrace(ProjectileTraceTypeId projectileTraceTypeId) =>
            _projectileTraces.TryGetValue(projectileTraceTypeId, out ProjectileTraceStaticData staticData)
                ? staticData
                : null;

        // public WindowStaticData ForWindow(WindowId windowId) =>
        //     _windows.TryGetValue(windowId, out WindowStaticData windowData)
        //         ? windowData
        //         : null;
    }
}