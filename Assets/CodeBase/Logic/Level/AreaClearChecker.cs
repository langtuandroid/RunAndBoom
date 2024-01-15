using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.Logic.Level
{
    public class AreaClearChecker : MonoBehaviour
    {
        private AreaTypeId _areaTypeId;
        private List<EnemyHealth> _enemyHealths;

        public void Initialize(AreaTypeId areaTypeId)
        {
            _areaTypeId = areaTypeId;
            Debug.Log($"Initialize areaTypeId {_areaTypeId}");
        }

        public void InitializeEnemyHealths() =>
            _enemyHealths = new List<EnemyHealth>();

        public void AddEnemy(EnemyHealth enemyHealth)
        {
            Debug.Log($"AddEnemy areaTypeId {_areaTypeId}");
            Debug.Log($"AddEnemy enemyHealth {enemyHealth}");
            _enemyHealths.Add(enemyHealth);
        }

        public void AddEnemy(EnemyHealth enemyHealth, AreaTypeId areaTypeId)
        {
            Debug.Log($"AddEnemy areaTypeId {_areaTypeId}");
            Debug.Log($"AddEnemy enemyHealth {enemyHealth}");
            _areaTypeId = areaTypeId;
            _enemyHealths.Add(enemyHealth);
        }

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