using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Enemy.Attacks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.Registrator;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Elements.Enemy;
using CodeBase.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRegistratorService _registratorService;
        private readonly IGameFactory _gameFactory;
        private Transform _spawnersRoot;

        public EnemyFactory(IAssets assets, IStaticDataService staticData, IRegistratorService registratorService, IGameFactory gameFactory)
        {
            _assets = assets;
            _staticData = staticData;
            _registratorService = registratorService;
            _gameFactory = gameFactory;
        }

        public async void CreateSpawnersRoot()
        {
            GameObject prefab = await _assets.Instantiate(AssetAddresses.SpawnersRoot);
            _spawnersRoot = prefab.transform;
        }

        public async Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddresses.Spawner);
            GameObject spawnerObject = _registratorService.InstantiateRegistered(prefab, at);
            spawnerObject.transform.SetParent(_spawnersRoot);
            SpawnPoint spawner = spawnerObject.GetComponent<SpawnPoint>();
            spawner.Construct(enemyTypeId);
            spawner.Initialize();
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(typeId);
            EnemyWeaponStaticData enemyWeaponStaticData = _staticData.ForEnemyWeapon(enemyData.EnemyWeaponTypeId);

            GameObject enemy = await _registratorService.InstantiateRegisteredAsync(typeId.ToString(), parent);
            _registratorService.RegisterProgressWatchers(enemy);
            enemy.GetComponentInChildren<EnemyWeaponAppearance>()?.Construct(enemyWeaponStaticData);
            enemy.GetComponent<EnemyDeath>().SetReward(enemyData.Reward);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            enemy.GetComponent<AgentMoveToHero>().Construct(_gameFactory.GetHero().transform);
            enemy.GetComponent<RotateToHero>().Construct(_gameFactory.GetHero().transform);
            enemy.GetComponent<Aggro>().Construct(enemyData.AttackCooldown);
            enemy.GetComponent<CheckAttackRange>().Construct(enemyData.EffectiveDistance);
            enemy.GetComponentInChildren<TargetMovement>().Hide();
            ConstructEnemyAttack(typeId, enemyData, enemy);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.SetMaxHealth(enemyData.Hp);

            return enemy;
        }

        private void ConstructEnemyAttack(EnemyTypeId typeId, EnemyStaticData enemyData, GameObject enemy)
        {
            Attack attack = enemy.GetComponent<Attack>();

            switch (typeId)
            {
                case EnemyTypeId.WithBat:
                    (attack as WithBatAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform, attackCooldown: enemyData.AttackCooldown,
                        cleavage: enemyData.Cleavage, effectiveDistance: enemyData.EffectiveDistance, damage: enemyData.Damage);
                    break;
                case EnemyTypeId.WithPistol:
                    (attack as WithPistolAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform, attackCooldown: enemyData.AttackCooldown);
                    break;
                case EnemyTypeId.WithShotgun:
                    (attack as WithShotgunAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform, attackCooldown: enemyData.AttackCooldown);
                    break;
            }
        }
    }
}