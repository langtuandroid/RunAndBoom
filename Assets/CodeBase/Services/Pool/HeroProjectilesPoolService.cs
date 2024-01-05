using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Constructor;
using CodeBase.StaticData.Hits;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Pool
{
    public class HeroProjectilesPoolService : IHeroProjectilesPoolService
    {
        private const int InitialCapacity = 4;
        private const string GrenadeTag = "Grenade";
        private const string RocketLauncherRocketTag = "RocketLauncherRocket";
        private const string RpgRocketTag = "RpgRocket";
        private const string BombTag = "Bomb";

        private IAssets _assets;
        private IConstructorService _constructorService;
        private Transform _root;
        private GameObject _gameObject;
        private ObjectPool<GameObject> _heroGrenadesPool;
        private ObjectPool<GameObject> _heroRpgRocketsPool;
        private ObjectPool<GameObject> _heroRocketLauncherRocketsPool;
        private ObjectPool<GameObject> _heroBombsPool;
        private GameObject _projectile;
        private GameObject _grenadePrefab;
        private GameObject _rpgRocketPrefab;
        private GameObject _rocketLauncherRocketPrefab;
        private GameObject _bombPrefab;

        public HeroProjectilesPoolService(IAssets assets, IConstructorService constructorService)
        {
            _assets = assets;
            _constructorService = constructorService;
        }

        public async void GenerateObjects()
        {
            Debug.Log("HeroProjectilesPoolService GenerateObjects");
            // if (_heroGrenadesPool != null)
            //     _heroGrenadesPool.Dispose();
            //
            // if (_heroRpgRocketsPool != null)
            //     _heroRpgRocketsPool.Dispose();
            //
            // if (_heroRocketLauncherRocketsPool != null)
            //     _heroRocketLauncherRocketsPool.Dispose();
            //
            // if (_heroBombsPool != null)
            //     _heroBombsPool.Dispose();
            Debug.Log($"root {_root}");
            Debug.Log($"grenadePrefab {_grenadePrefab}");
            Debug.Log($"rpgRocketPrefab {_rpgRocketPrefab}");
            Debug.Log($"rocketLauncherRocketPrefab {_rocketLauncherRocketPrefab}");
            Debug.Log($"bombPrefab {_bombPrefab}");

            if (_root == null)
            {
                GameObject root = await _assets.Load<GameObject>(AssetAddresses.HeroProjectilesRoot);
                _gameObject = Object.Instantiate(root);
                _root = _gameObject.transform;
            }

            if (_grenadePrefab == null)
            {
                _grenadePrefab = await _assets.Instantiate(AssetAddresses.Grenade, _root);
                _grenadePrefab.SetActive(false);
            }

            if (_rpgRocketPrefab == null)
            {
                _rpgRocketPrefab = await _assets.Instantiate(AssetAddresses.RpgRocket, _root);
                _rpgRocketPrefab.SetActive(false);
            }

            if (_rocketLauncherRocketPrefab == null)
            {
                _rocketLauncherRocketPrefab = await _assets.Instantiate(AssetAddresses.RocketLauncherRocket, _root);
                _rocketLauncherRocketPrefab.SetActive(false);
            }

            if (_bombPrefab == null)
            {
                _bombPrefab = await _assets.Instantiate(AssetAddresses.Bomb, _root);
                _bombPrefab.SetActive(false);
            }

            Debug.Log($"grenadePrefab {_grenadePrefab}");
            Debug.Log($"rpgRocketPrefab {_rpgRocketPrefab}");
            Debug.Log($"rocketLauncherRocketPrefab {_rocketLauncherRocketPrefab}");
            Debug.Log($"bombPrefab {_bombPrefab}");

            _heroGrenadesPool = new ObjectPool<GameObject>(GetGrenade, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            _heroRpgRocketsPool = new ObjectPool<GameObject>(GetRpgRocket, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            _heroRocketLauncherRocketsPool = new ObjectPool<GameObject>(GetRocketLauncherRocket, GetFromPool,
                ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            _heroBombsPool = new ObjectPool<GameObject>(GetBomb, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            Debug.Log($"heroGrenadesPool {_heroGrenadesPool}");
            Debug.Log($"heroRpgRocketsPool {_heroRpgRocketsPool}");
            Debug.Log($"heroRocketLauncherRocketsPool {_heroRocketLauncherRocketsPool}");
            Debug.Log($"heroBombsPool {_heroBombsPool}");
        }

        private GameObject GetGrenade()
        {
            // Debug.Log($"grenadePrefab {_grenadePrefab}");
            // if (_grenadePrefab == null)
            //     CreatePrefab(_grenadePrefab, AssetAddresses.Grenade);

            Debug.Log($"grenadePrefab {_grenadePrefab}");
            _projectile = Object.Instantiate(_grenadePrefab);
            Debug.Log($"constructorService {_constructorService}");
            _constructorService.ConstructHeroProjectile(this,
                AllServices.Container.Single<IEnemyProjectilesPoolService>(), _projectile, ProjectileTypeId.Grenade,
                BlastTypeId.Grenade,
                HeroWeaponTypeId.GrenadeLauncher);
            Debug.Log($"GetGrenade {_projectile}");
            return _projectile;
        }

        private GameObject GetRpgRocket()
        {
            // Debug.Log($"rpgRocketPrefab {_rpgRocketPrefab}");
            // if (_rpgRocketPrefab == null)
            //     CreatePrefab(_rpgRocketPrefab, AssetAddresses.RpgRocket);

            Debug.Log($"rpgRocketPrefab {_rpgRocketPrefab}");
            _projectile = Object.Instantiate(_rpgRocketPrefab);
            Debug.Log($"constructorService {_constructorService}");
            _constructorService.ConstructHeroProjectile(this,
                AllServices.Container.Single<IEnemyProjectilesPoolService>(), _projectile, ProjectileTypeId.RpgRocket,
                BlastTypeId.RpgRocket, HeroWeaponTypeId.RPG);
            Debug.Log($"GetRpgRocket {_projectile}");
            return _projectile;
        }

        private async void CreatePrefab(GameObject prefab, string address) =>
            prefab = await _assets.Instantiate(address);

        private GameObject GetRocketLauncherRocket()
        {
            _projectile = Object.Instantiate(_rocketLauncherRocketPrefab);
            _constructorService.ConstructHeroProjectile(this,
                AllServices.Container.Single<IEnemyProjectilesPoolService>(), _projectile,
                ProjectileTypeId.RocketLauncherRocket,
                BlastTypeId.RocketLauncherRocket, HeroWeaponTypeId.RocketLauncher);
            Debug.Log($"GetRocketLauncherRocket {_projectile}");
            return _projectile;
        }

        private GameObject GetBomb()
        {
            _projectile = Object.Instantiate(_bombPrefab);
            _constructorService.ConstructHeroProjectile(this,
                AllServices.Container.Single<IEnemyProjectilesPoolService>(), _projectile, ProjectileTypeId.Bomb,
                BlastTypeId.Bomb,
                HeroWeaponTypeId.Mortar);
            Debug.Log($"GetBomb {_projectile}");
            return _projectile;
        }

        public GameObject GetFromPool(HeroWeaponTypeId typeId)
        {
            Debug.Log("GetFromPool");
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    Debug.Log($"heroGrenadesPool {_heroGrenadesPool}");
                    return _heroGrenadesPool.Get();

                case HeroWeaponTypeId.RPG:
                    Debug.Log($"heroRpgRocketsPool {_heroRpgRocketsPool}");
                    return _heroRpgRocketsPool.Get();

                case HeroWeaponTypeId.RocketLauncher:
                    Debug.Log($"heroRocketLauncherRocketsPool {_heroRocketLauncherRocketsPool}");
                    return _heroRocketLauncherRocketsPool.Get();

                case HeroWeaponTypeId.Mortar:
                    Debug.Log($"heroBombsPool {_heroBombsPool}");
                    return _heroBombsPool.Get();
            }

            return null;
        }

        public void Return(GameObject pooledObject)
        {
            if (pooledObject.CompareTag(GrenadeTag))
                _heroGrenadesPool.Release(pooledObject);
            else if (pooledObject.CompareTag(RocketLauncherRocketTag))
                _heroRocketLauncherRocketsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(RpgRocketTag))
                _heroRpgRocketsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(BombTag))
                _heroBombsPool.Release(pooledObject);
            else
                return;
        }

        private void ReturnToPool(GameObject pooledObject)
        {
            pooledObject.transform.SetParent(_root);
            pooledObject.SetActive(false);
        }

        private void GetFromPool(GameObject pooledObject)
        {
            pooledObject.transform.SetParent(null);
            pooledObject.SetActive(true);
        }

        private void DestroyPooledObject(GameObject pooledObject) =>
            Object.Destroy(pooledObject);
    }
}