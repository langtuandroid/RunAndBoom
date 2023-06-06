using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Enemy.Attacks;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.Registrator;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Weapons;
using CodeBase.Weapons;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRegistratorService _registratorService;
        private readonly IGameFactory _gameFactory;
        private Transform _spawnersRoot;

        public EnemyFactory(IAssets assets, IStaticDataService staticData, IRegistratorService registratorService,
            IGameFactory gameFactory)
        {
            _assets = assets;
            _staticData = staticData;
            _registratorService = registratorService;
            _gameFactory = gameFactory;
        }

        public async void CreateSpawnersRoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddresses.SpawnersRoot);
            GameObject gameObject = Object.Instantiate(prefab);
            _spawnersRoot = gameObject.transform;
            // GameObject prefab = await _assets.Instantiate(AssetAddresses.SpawnersRoot);
            // _spawnersRoot = prefab.transform;
        }

        public async Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddresses.Spawner);
            GameObject spawnerObject = _registratorService.InstantiateRegistered(prefab, at);
            SpawnPoint spawner = spawnerObject.GetComponent<SpawnPoint>();
            spawner.Construct(enemyTypeId);
            spawner.Initialize();
            spawnerObject.transform.SetParent(_spawnersRoot);
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(typeId);
            EnemyWeaponStaticData enemyWeaponStaticData = _staticData.ForEnemyWeapon(enemyData.EnemyWeaponTypeId);
            GameObject prefab = await _assets.Load<GameObject>(enemyData.PrefabReference);
            GameObject enemy = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
            EnemyDeath death = enemy.GetComponent<EnemyDeath>();
            enemy.GetComponentInChildren<EnemyWeaponAppearance>()?.Construct(death, enemyWeaponStaticData);
            enemy.GetComponent<EnemyDeath>()
                .Construct(_gameFactory.GetHero().GetComponent<HeroHealth>(), enemyData.Reward);
            enemy.GetComponent<AgentMoveToHero>().Construct(_gameFactory.GetHero().transform, enemyData.MoveSpeed,
                enemyData.IsMovable);
            enemy.GetComponent<RotateToHero>().Construct(_gameFactory.GetHero().transform);
            enemy.GetComponent<Aggro>().Construct(enemyData.FollowDistance);
            enemy.GetComponent<CheckAttackRange>().Construct(enemyData.AttackDistance);
            ConstructEnemyAttack(typeId, enemyData, enemy);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.Initial(enemyData.Hp);

            return enemy;
        }

        private void ConstructEnemyAttack(EnemyTypeId typeId, EnemyStaticData enemyData, GameObject enemy)
        {
            Attack attack = enemy.GetComponent<Attack>();

            switch (typeId)
            {
                case EnemyTypeId.WithBat:
                    (attack as WithBatAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform,
                        attackCooldown: enemyData.AttackCooldown, effectiveDistance: enemyData.AttackDistance,
                        damage: enemyData.Damage);
                    break;
                case EnemyTypeId.WithPistol:
                    (attack as WithPistolAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform,
                        attackCooldown: enemyData.AttackCooldown);
                    break;
                case EnemyTypeId.WithShotgun:
                    (attack as WithShotgunAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform,
                        attackCooldown: enemyData.AttackCooldown);
                    break;
                case EnemyTypeId.WithSMG:
                    (attack as WithPistolAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform,
                        attackCooldown: enemyData.AttackCooldown);
                    break;
                case EnemyTypeId.WithSniperRifle:
                    (attack as WithPistolAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform,
                        attackCooldown: enemyData.AttackCooldown);
                    break;
                case EnemyTypeId.WithMG:
                    (attack as WithPistolAttack)?.Construct(heroTransform: _gameFactory.GetHero().transform,
                        attackCooldown: enemyData.AttackCooldown);
                    break;
            }
        }
    }
}