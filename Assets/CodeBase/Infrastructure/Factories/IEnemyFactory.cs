using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IEnemyFactory : IService
    {
        void CreateSpawnersRoot();

        Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId
            // , AreaData area
        );

        Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent
            // , AreaData area
        );
    }
}