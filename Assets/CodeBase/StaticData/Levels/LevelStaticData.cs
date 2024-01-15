using System.Collections.Generic;
using CodeBase.Data.Progress;
using UnityEngine;

namespace CodeBase.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public SceneId Level;
        public bool InitializeHeroPosition;
        public Vector3 InitialHeroPosition;
        public int TargetPlayTime;
        public int MaxStarsScoreStandard;
        public int MaxStarsScoreAsian;

        public LevelTransferStaticData LevelTransfer;
        public List<EnemySpawnerData> EnemySpawners;

        public List<EnemySpawnerData> EnemyWithBatSpawners;
        public List<EnemySpawnerData> EnemyWithPistolSpawners;
        public List<EnemySpawnerData> EnemyWithShotgunSpawners;
        public List<EnemySpawnerData> EnemyWithSMGSpawners;
        public List<EnemySpawnerData> EnemyWithSRSpawners;
        public List<EnemySpawnerData> EnemyWithMGSpawners;

        // public List<AreaData> AreaDatas;
        // public List<AreaEnemiesContainer> AreaEnemiesContainers;
        // public List<AreaClearChecker> AreaClearCheckers;
    }
}