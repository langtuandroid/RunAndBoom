using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class AreaEnemiesContainer : MonoBehaviour
    {
        [SerializeField] private List<SpawnMarker> _spawnMarkers;

        private AreaController _areaController;
        private AreaTypeId _areaTypeId;

        public AreaTypeId AreaTypeId => _areaTypeId;
        public List<SpawnMarker> SpawnMarkers => _spawnMarkers;

        public void Initialize(AreaTypeId areaTypeId, AreaController areaController)
        {
            _areaTypeId = areaTypeId;
            _areaController = areaController;
        }

        public AreaData GetAreaStaticData()
        {
            return new AreaData(AreaTypeId, SpawnMarkers.Select(x => new SpawnMarkerData(x.EnemyTypeId,
                new Vector3Data(x.transform.position.x, x.transform.position.y,
                    x.transform.position.z))).ToList(), this, _areaController.AreaClearChecker);
        }
    }
}