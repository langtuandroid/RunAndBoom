using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Enemy.Attacks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.Registrator;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.ProjectileTrace;
using CodeBase.StaticData.Weapon;
using CodeBase.UI.Elements.Hud;
using CodeBase.Weapons;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRegistratorService _registratorService;
        private readonly IGameFactory _gameFactory;
        private readonly DiContainer _container;

        public EnemyFactory(IAssets assets, IStaticDataService staticData, IRegistratorService registratorService, IGameFactory gameFactory,
            DiContainer container)
        {
            _assets = assets;
            _staticData = staticData;
            _registratorService = registratorService;
            _gameFactory = gameFactory;
            _container = container;
        }

        public async Task CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddresses.Spawner);
            SpawnPoint spawner = _registratorService.InstantiateRegistered(prefab, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct(this, enemyTypeId);
            spawner.Initialize();
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(typeId);
            EnemyWeaponStaticData enemyWeaponStaticData = _staticData.ForEnemyWeapon(enemyData.EnemyWeaponTypeId);
            ProjectileTraceStaticData projectileTraceStaticData = _staticData.ForProjectileTrace(enemyWeaponStaticData.ProjectileTraceTypeId);

            // GameObject enemy = await _registratorService.InstantiateRegisteredAsync(typeId.ToString(), parent);

            var prefab = await _registratorService.LoadRegisteredAsync(typeId.ToString());
            GameObject enemy = _container.InstantiatePrefab(prefab, parent);
            _registratorService.RegisterProgressWatchers(enemy);

            enemy.GetComponentInChildren<EnemyWeaponAppearance>()?.Construct(enemyWeaponStaticData, projectileTraceStaticData);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.SetMaxHealth(enemyData.Hp);

            enemy.GetComponent<EnemyDeath>().SetReward(enemyData.Reward);

            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            enemy.GetComponent<AgentMoveToHero>().Construct(_gameFactory.GetHero().transform);
            enemy.GetComponent<RotateToHero>().Construct(_gameFactory.GetHero().transform);
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