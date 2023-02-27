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
        private const string StaticDataHeroWeaponsPath = "StaticData/HeroWeapons";
        private const string StaticDataEnemyWeaponsPath = "StaticData/EnemyWeapons";
        private const string StaticDataLevelsPath = "StaticData/Levels";

        private const string StaticDataProjectileTracesPath = "StaticData/ProjectilesTraces";
        // private const string StaticDataWindowsPath = "StaticData/Windows";

        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<HeroWeaponTypeId, HeroWeaponStaticData> _heroWeapons;
        private Dictionary<EnemyWeaponTypeId, EnemyWeaponStaticData> _enemyWeapons;
        private Dictionary<string, LevelStaticData> _levels;

        private Dictionary<ProjectileTraceTypeId, ProjectileTraceStaticData> _projectileTraces;
        // private Dictionary<WindowId, WindowStaticData> _windows;

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);

            _heroWeapons = Resources
                .LoadAll<HeroWeaponStaticData>(StaticDataHeroWeaponsPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);

            _enemyWeapons = Resources
                .LoadAll<EnemyWeaponStaticData>(StaticDataEnemyWeaponsPath)
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

        public HeroWeaponStaticData ForHeroWeapon(HeroWeaponTypeId typeId) =>
            _heroWeapons.TryGetValue(typeId, out HeroWeaponStaticData staticData)
                ? staticData
                : null;

        public EnemyWeaponStaticData ForEnemyWeapon(EnemyWeaponTypeId typeId) =>
            _enemyWeapons.TryGetValue(typeId, out EnemyWeaponStaticData staticData)
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