using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using CodeBase.StaticData.Enemies;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;

        private IEnemyFactory _factory;
        // private AreaData _areaData;

        private void Awake() =>
            _factory = AllServices.Container.Single<IEnemyFactory>();

        public void Construct(EnemyTypeId enemyTypeId
            // , AreaData areaData
        )
        {
            _enemyTypeId = enemyTypeId;
            // _areaData = areaData;
        }

        public void Initialize() =>
            Spawn();

        private async void Spawn() =>
            await _factory.CreateEnemy(_enemyTypeId, transform
                // , _areaData
            );
    }
}