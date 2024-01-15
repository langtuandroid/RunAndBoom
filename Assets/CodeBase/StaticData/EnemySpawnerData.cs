using System;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData.Enemies;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public EnemyTypeId EnemyTypeId;

        // public AreaTypeId AreaTypeId;
        public Vector3Data Position;

        public EnemySpawnerData(EnemyTypeId enemyTypeId
            // , AreaTypeId areaTypeId
            , Vector3Data position)
        {
            EnemyTypeId = enemyTypeId;
            // AreaTypeId = areaTypeId;
            Position = position;
        }
    }
}