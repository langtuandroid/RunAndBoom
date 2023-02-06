using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] public EnemyTypeId enemyTypeId;
        public string Id { get; set; }

        private IGameFactory _factory;

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        public void Initialize()
        {
            Spawn();
        }

        private async void Spawn()
        {
            GameObject enemy = await _factory.CreateEnemy(enemyTypeId, transform);
        }
    }
}