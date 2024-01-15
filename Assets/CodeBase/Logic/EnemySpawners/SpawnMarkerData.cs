using System;
using CodeBase.StaticData.Enemies;

namespace CodeBase.Logic.EnemySpawners
{
    [Serializable]
    public class SpawnMarkerData
    {
        public EnemyTypeId EnemyTypeId;
        public Vector3Data Position;

        public SpawnMarkerData(EnemyTypeId enemyTypeId, Vector3Data position)
        {
            EnemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}