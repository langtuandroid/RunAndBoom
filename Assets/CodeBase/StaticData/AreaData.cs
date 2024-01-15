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
        public List<SpawnMarkerData> SpawnMarkerDatas;
        [HideInInspector] public AreaEnemiesContainer AreaEnemiesContainer;
        [HideInInspector] public AreaClearChecker AreaClearChecker;

        public AreaData(AreaTypeId areaTypeId, List<SpawnMarkerData> spawnMarkerDatas,
            AreaEnemiesContainer areaEnemiesContainer, AreaClearChecker areaClearChecker)
        {
            AreaTypeId = areaTypeId;
            SpawnMarkerDatas = spawnMarkerDatas;
            AreaEnemiesContainer = areaEnemiesContainer;
            AreaClearChecker = areaClearChecker;
        }
    }
}