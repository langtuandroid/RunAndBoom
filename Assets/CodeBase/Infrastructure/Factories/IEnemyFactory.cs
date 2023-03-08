using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IEnemyFactory : IService
    {
        Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId);
        Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent);
    }
}