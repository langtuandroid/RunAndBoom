using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.Level
{
    public class AreaClearChecker : MonoBehaviour
    {
        [FormerlySerializedAs("_areaTypeId")] public AreaTypeId AreaTypeId;

        [FormerlySerializedAs("_spawnMarkers")]
        public List<SpawnMarker> SpawnMarkers;

        public AreaData AreaData;

        private List<EnemyHealth> _enemyHealths = new List<EnemyHealth>();

        public void InitializeAreaStaticData() =>
            AreaData = new AreaData(AreaTypeId, SpawnMarkers, this);

        public void AddEnemy(EnemyHealth enemyHealth) =>
            _enemyHealths.Add(enemyHealth);

        public bool IsAreaClear()
        {
            for (int i = 0; i < _enemyHealths.Count; i++)
            {
                if (_enemyHealths[i].Current > 0)
                    return false;
            }

            return true;
        }
    }
}