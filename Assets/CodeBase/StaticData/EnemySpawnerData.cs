using System;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public EnemyTypeId EnemyTypeId;
        public Vector3 Position;

        public EnemySpawnerData(EnemyTypeId enemyTypeId, Vector3 position)
        {
            EnemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}