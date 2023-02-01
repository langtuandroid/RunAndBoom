using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Registrator;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.UI.Elements.Hud;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IRegistratorService _registratorService;
        private GameObject _heroGameObject;

        public List<IProgressReader> ProgressReaders { get; set; } = new List<IProgressReader>();
        public List<IProgressSaver> ProgressWriters { get; set; } = new List<IProgressSaver>();

        public GameFactory(IAssets assets, IPlayerProgressService progressService, IStaticDataService staticData,
            IRegistratorService registratorService)
        {
            _assets = assets;
            _progressService = progressService;
            _staticData = staticData;
            _registratorService = registratorService;

            SetProgressReadersWriters(registratorService);
        }

        private void SetProgressReadersWriters(IRegistratorService registratorService)
        {
            ProgressReaders = registratorService.ProgressReaders;
            ProgressWriters = registratorService.ProgressWriters;
        }

        public async Task WarmUp()
        {
            // await _assets.Load<GameObject>(AssetAddresses.Spawner);
        }

        public async Task<GameObject> CreateHero(Vector3 at)
        {
            _heroGameObject = await _registratorService.InstantiateRegisteredAsync(AssetAddresses.Hero, at);
            // GameObject heroRotating = _heroGameObject.transform.GetChild(0).gameObject;
            // HeroShooting heroShooting = heroRotating.GetComponent<HeroShooting>();
            return _heroGameObject;
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForEnemy(typeId);

            GameObject enemy = await _registratorService.InstantiateRegisteredAsync(typeId.ToString(), parent);
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;
            enemy.GetComponent<AgentMoveToHero>()?.Construct(_heroGameObject.transform);
            enemy.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

            Attack attack = enemy.GetComponent<Attack>();
            attack.Construct(_heroGameObject.transform);
            attack.Damage = enemyData.Damage;
            attack.Cleavage = enemyData.Cleavage;
            attack.EffectiveDistance = enemyData.EffectiveDistance;

            return enemy;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.CleanUp();
        }

        public async Task CreateSpawner(string spawnerId, Vector3 at, EnemyTypeId enemyTypeId)
        {
            // GameObject prefab = await _assets.Load<GameObject>(AssetAddresses.Spawner);
            // SpawnPoint spawner = _registratorService.InstantiateRegistered(prefab, at)
            //     .GetComponent<SpawnPoint>();
            // spawner.Construct(this);
            // spawner.Initialize();
            // spawner.Id = spawnerId;
            // spawner.EnemyTypeId = enemyTypeId;
        }
    }
}