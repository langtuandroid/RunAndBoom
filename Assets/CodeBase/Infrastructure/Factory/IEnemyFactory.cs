using System.Threading.Tasks;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEnemyFactory
    {
        Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId);
        Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent);
    }
}