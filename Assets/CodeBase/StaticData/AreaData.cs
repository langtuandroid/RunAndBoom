using System;
using System.Collections.Generic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Level;
using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class AreaData
    {
        public AreaTypeId AreaTypeId;
        public List<SpawnMarker> SpawnMarkers;
        [HideInInspector] public AreaClearChecker AreaClearChecker;

        public AreaData(AreaTypeId areaTypeId, List<SpawnMarker> spawnMarkers, AreaClearChecker areaClearChecker)
        {
            AreaTypeId = areaTypeId;
            SpawnMarkers = spawnMarkers;
            AreaClearChecker = areaClearChecker;
        }
    }
}