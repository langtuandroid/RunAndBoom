using System.Linq;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                            new EnemySpawnerData(x.enemyTypeId, x.transform.position))
                        .ToList();

                levelData.EnemyWithBatSpawners.Clear();
                levelData.EnemyWithPistolSpawners.Clear();
                levelData.EnemyWithShotgunSpawners.Clear();
                levelData.EnemyWithSMGSpawners.Clear();
                levelData.EnemyWithSRSpawners.Clear();
                levelData.EnemyWithMGSpawners.Clear();

                foreach (SpawnMarker spawnMarker in findObjectsOfType)
                {
                    switch (spawnMarker.enemyTypeId)
                    {
                        case EnemyTypeId.WithBat:
                            levelData.EnemyWithBatSpawners.Add(new EnemySpawnerData(spawnMarker.enemyTypeId,
                                spawnMarker.transform.position));
                            break;
                        case EnemyTypeId.WithPistol:
                            levelData.EnemyWithPistolSpawners.Add(new EnemySpawnerData(spawnMarker.enemyTypeId,
                                spawnMarker.transform.position));
                            break;
                        case EnemyTypeId.WithShotgun:
                            levelData.EnemyWithShotgunSpawners.Add(new EnemySpawnerData(spawnMarker.enemyTypeId,
                                spawnMarker.transform.position));
                            break;
                        case EnemyTypeId.WithSMG:
                            levelData.EnemyWithSMGSpawners.Add(new EnemySpawnerData(spawnMarker.enemyTypeId,
                                spawnMarker.transform.position));
                            break;
                        case EnemyTypeId.WithSniperRifle:
                            levelData.EnemyWithSRSpawners.Add(new EnemySpawnerData(spawnMarker.enemyTypeId,
                                spawnMarker.transform.position));
                            break;
                        case EnemyTypeId.WithMG:
                            levelData.EnemyWithMGSpawners.Add(new EnemySpawnerData(spawnMarker.enemyTypeId,
                                spawnMarker.transform.position));
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