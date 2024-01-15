using System.Linq;
using CodeBase.Data;
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

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                SpawnMarker[] findObjectsOfType = FindObjectsOfType<SpawnMarker>();
                levelData.EnemySpawners =
                    findObjectsOfType
                        .Select(x =>
                            new EnemySpawnerData(x.EnemyTypeId, x.transform.position.AsVectorData()))
                        .ToList();

                levelData.EnemyWithBatSpawners.Clear();
                levelData.EnemyWithPistolSpawners.Clear();
                levelData.EnemyWithShotgunSpawners.Clear();
                levelData.EnemyWithSMGSpawners.Clear();
                levelData.EnemyWithSRSpawners.Clear();
                levelData.EnemyWithMGSpawners.Clear();

                foreach (SpawnMarker spawnMarker in findObjectsOfType)
                {
                    switch (spawnMarker.EnemyTypeId)
                    {
                        case EnemyTypeId.WithBat:
                            levelData.EnemyWithBatSpawners.Add(new EnemySpawnerData(spawnMarker.EnemyTypeId,
                                spawnMarker.transform.position.AsVectorData()));
                            break;
                        case EnemyTypeId.WithPistol:
                            levelData.EnemyWithPistolSpawners.Add(new EnemySpawnerData(spawnMarker.EnemyTypeId,
                                spawnMarker.transform.position.AsVectorData()));
                            break;
                        case EnemyTypeId.WithShotgun:
                            levelData.EnemyWithShotgunSpawners.Add(new EnemySpawnerData(spawnMarker.EnemyTypeId,
                                spawnMarker.transform.position.AsVectorData()));
                            break;
                        case EnemyTypeId.WithSMG:
                            levelData.EnemyWithSMGSpawners.Add(new EnemySpawnerData(spawnMarker.EnemyTypeId,
                                spawnMarker.transform.position.AsVectorData()));
                            break;
                        case EnemyTypeId.WithSniperRifle:
                            levelData.EnemyWithSRSpawners.Add(new EnemySpawnerData(spawnMarker.EnemyTypeId,
                                spawnMarker.transform.position.AsVectorData()));
                            break;
                        case EnemyTypeId.WithMG:
                            levelData.EnemyWithMGSpawners.Add(new EnemySpawnerData(spawnMarker.EnemyTypeId,
                                spawnMarker.transform.position.AsVectorData()));
                            break;
                    }
                }

                if (levelData.InitializeHeroPosition)
                    levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}