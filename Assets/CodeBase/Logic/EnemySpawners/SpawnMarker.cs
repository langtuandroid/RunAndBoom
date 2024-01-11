using CodeBase.StaticData.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        public UniqueId UniqueId;
        [FormerlySerializedAs("enemyTypeId")] public EnemyTypeId EnemyTypeId;
        public AreaTypeId AreaTypeId;
    }
}