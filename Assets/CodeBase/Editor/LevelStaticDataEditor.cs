using System.Collections.Generic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Levels;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";

        private EnemySpawnerData _enemySpawnerData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                // AreaClearChecker[] areaClearCheckers = FindObjectsOfType<AreaClearChecker>();

                // for (int i = 0; i < areaClearCheckers.Length; i++) 
                //     areaClearCheckers[i].InitializeAreaStaticData();

                // levelData.Areas = areaClearCheckers
                //     .Select(x => x.AreaData)
                //     .ToList();

                levelData.EnemySpawners = new List<EnemySpawnerData>();
                levelData.EnemyWithBatSpawners.Clear();
                levelData.EnemyWithPistolSpawners.Clear();
                levelData.EnemyWithShotgunSpawners.Clear();
                levelData.EnemyWithSMGSpawners.Clear();
                levelData.EnemyWithSRSpawners.Clear();
                levelData.EnemyWithMGSpawners.Clear();

                foreach (AreaData area in levelData.Areas)
                {
                    foreach (SpawnMarker spawnMarker in area.SpawnMarkers)
                    {
                        _enemySpawnerData = new EnemySpawnerData(spawnMarker.EnemyTypeId, area.AreaTypeId,
                            spawnMarker.transform.position);

                        levelData.EnemySpawners.Add(_enemySpawnerData);

                        switch (spawnMarker.EnemyTypeId)
                        {
                            case EnemyTypeId.WithBat:
                                levelData.EnemyWithBatSpawners.Add(_enemySpawnerData);
                                break;
                            case EnemyTypeId.WithPistol:
                                levelData.EnemyWithPistolSpawners.Add(_enemySpawnerData);
                                break;
                            case EnemyTypeId.WithShotgun:
                                levelData.EnemyWithShotgunSpawners.Add(_enemySpawnerData);
                                break;
                            case EnemyTypeId.WithSMG:
                                levelData.EnemyWithSMGSpawners.Add(_enemySpawnerData);
                                break;
                            case EnemyTypeId.WithSniperRifle:
                                levelData.EnemyWithSRSpawners.Add(_enemySpawnerData);
                                break;
                            case EnemyTypeId.WithMG:
                                levelData.EnemyWithMGSpawners.Add(_enemySpawnerData);
                                break;
                        }
                    }
                }

                if (levelData.InitializeHeroPosition)
                    levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}