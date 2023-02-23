using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Enemy.Attacks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Registrator;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.UI.Elements.Hud;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IRegistratorService _registratorService;
        private readonly DiContainer _container;
        private GameObject _heroGameObject;

        public List<IProgressReader> ProgressReaders { get; set; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; set; } = new List<IProgressSaver>();

        public GameFactory(IAssets assets, IPlayerProgressService progressService, IStaticDataService staticData,
            IRegistratorService registratorService, DiContainer container)
        {
            _assets = assets;
            _progressService = progressService;
            _staticData = staticData;
            _registratorService = registratorService;
            _container = container;

            SetProgressReadersWriters(registratorService);
        }

        private void SetProgressReadersWriters(IRegistratorService registratorService)
        {
            ProgressReaders = registratorService.ProgressReaders;
            ProgressWriters = registratorService.ProgressWriters;
        }

        public async Task WarmUp()
        {
            _assets.Initialize();
            // await _assets.Load<GameObject>(AssetAddresses.Spawner);
        }

        public async Task<GameObject> CreateHero(Vector3 at)
        {
            var prefab = await _registratorService.LoadRegisteredAsync(AssetAddresses.Hero);
            _heroGameObject = _container.InstantiatePrefab(prefab, at.AddY(0.5f), Quaternion.identity, null);
            _registratorService.RegisterProgressWatchers(_heroGameObject);
            return _heroGameObject;
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(typeId);

            GameObject enemy = await _registratorService.InstantiateRegisteredAsync(typeId.ToString());
            // var prefab = await _registratorService.LoadRegisteredAsync(typeId.ToString());
            // GameObject enemy = _container.InstantiatePrefab(prefab, parent);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            enemy.GetComponent<AgentMoveToHero>().Construct(_heroGameObject.transform);
            enemy.GetComponent<RotateToHero>().Construct(_heroGameObject.transform);
            enemy.GetComponent<Aggro>().Construct(enemyData.AttackCooldown);
            enemy.GetComponent<CheckAttackRange>().Construct(enemyData.EffectiveDistance);
            enemy.GetComponentInChildren<TargetMovement>().Hide();

            ConstructEnemyAttack(typeId, enemyData, enemy);

            return enemy;
        }

        private void ConstructEnemyAttack(EnemyTypeId typeId, EnemyStaticData enemyData, GameObject enemy)
        {
            Attack attack = enemy.GetComponent<Attack>();

            switch (typeId)
            {
                case EnemyTypeId.WithBat:
                    (attack as WithBatAttack)?.Construct(heroTransform: _heroGameObject.transform, attackCooldown: enemyData.AttackCooldown,
                        cleavage: enemyData.Cleavage, effectiveDistance: enemyData.EffectiveDistance, damage: enemyData.Damage);
                    break;
                case EnemyTypeId.WithPistol:
                    (attack as WithPistolAttack)?.Construct(heroTransform: _heroGameObject.transform, attackCooldown: enemyData.AttackCooldown,
                        cleavage: enemyData.Cleavage, effectiveDistance: enemyData.EffectiveDistance, damage: enemyData.Damage);
                    break;
                default:
                    break;
            }
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.CleanUp();
        }

        public async Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddresses.Spawner);
            SpawnPoint spawner = _registratorService.InstantiateRegistered(prefab, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct(this, enemyTypeId);
            spawner.Initialize();
        }
    }
}