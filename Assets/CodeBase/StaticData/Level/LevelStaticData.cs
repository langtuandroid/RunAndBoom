using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public bool InitializePersonPosition;

        public Vector3 InitialPersonPosition;

        // public LevelTransferStaticData LevelTransfer;
        public List<EnemySpawnerData> EnemySpawners;
    }
}