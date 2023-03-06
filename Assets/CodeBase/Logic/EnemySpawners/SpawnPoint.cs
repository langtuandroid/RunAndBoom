using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;

        private IEnemyFactory _factory;

        public void Construct(IEnemyFactory factory, EnemyTypeId enemyTypeId)
        {
            _factory = factory;
            _enemyTypeId = enemyTypeId;
        }

        public void Initialize() =>
            Spawn();

        private async void Spawn() =>
            await _factory.CreateEnemy(_enemyTypeId, transform);
    }
}