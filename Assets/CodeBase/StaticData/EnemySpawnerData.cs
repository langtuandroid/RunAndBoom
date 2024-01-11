using System;
using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public EnemyTypeId EnemyTypeId;
        public AreaTypeId AreaTypeId;
        public Vector3 Position;

        public EnemySpawnerData(EnemyTypeId enemyTypeId, AreaTypeId areaTypeId, Vector3 position)
        {
            EnemyTypeId = enemyTypeId;
            AreaTypeId = areaTypeId;
            Position = position;
        }
    }
}