using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Constructor;
using CodeBase.Services.StaticData;
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
        private IStaticDataService _staticDataService;
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

        public HeroProjectilesPoolService(IAssets assets, IConstructorService constructorService,
            IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assets = assets;
            _constructorService = constructorService;
        }

        public async void GenerateObjects()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.HeroProjectilesRoot);
            _gameObject = Object.Instantiate(root);
            _root = _gameObject.transform;
            _grenadePrefab = await _assets.Instantiate(AssetAddresses.Grenade, _root);
            _rpgRocketPrefab = await _assets.Instantiate(AssetAddresses.RpgRocket, _root);
            _rocketLauncherRocketPrefab = await _assets.Instantiate(AssetAddresses.RocketLauncherRocket, _root);
            _bombPrefab = await _assets.Instantiate(AssetAddresses.Bomb, _root);
            _grenadePrefab.SetActive(false);
            _rpgRocketPrefab.SetActive(false);
            _rocketLauncherRocketPrefab.SetActive(false);
            _bombPrefab.SetActive(false);

            _heroGrenadesPool = new ObjectPool<GameObject>(GetGrenade, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            _heroRpgRocketsPool = new ObjectPool<GameObject>(GetRpgRocket, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            _heroRocketLauncherRocketsPool = new ObjectPool<GameObject>(GetRocketLauncherRocket, GetFromPool,
                ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);

            _heroBombsPool = new ObjectPool<GameObject>(GetBomb, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 2);
        }

        private GameObject GetGrenade()
        {
            _projectile = Object.Instantiate(_grenadePrefab);
            _constructorService.ConstructHeroProjectile(_projectile, ProjectileTypeId.Grenade, BlastTypeId.Grenade,
                HeroWeaponTypeId.GrenadeLauncher);
            return _projectile;
        }

        private GameObject GetRpgRocket()
        {
            _projectile = Object.Instantiate(_rpgRocketPrefab);
            _constructorService.ConstructHeroProjectile(_projectile, ProjectileTypeId.RpgRocket,
                BlastTypeId.RpgRocket, HeroWeaponTypeId.RPG);
            return _projectile;
        }

        private GameObject GetRocketLauncherRocket()
        {
            _projectile = Object.Instantiate(_rocketLauncherRocketPrefab);
            _constructorService.ConstructHeroProjectile(_projectile, ProjectileTypeId.RocketLauncherRocket,
                BlastTypeId.RocketLauncherRocket, HeroWeaponTypeId.RocketLauncher);
            return _projectile;
        }

        private GameObject GetBomb()
        {
            _projectile = Object.Instantiate(_bombPrefab);
            _constructorService.ConstructHeroProjectile(_projectile, ProjectileTypeId.Bomb, BlastTypeId.Bomb,
                HeroWeaponTypeId.Mortar);
            return _projectile;
        }

        public GameObject GetFromPool(HeroWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case HeroWeaponTypeId.GrenadeLauncher:
                    return _heroGrenadesPool.Get();

                case HeroWeaponTypeId.RPG:
                    return _heroRpgRocketsPool.Get();

                case HeroWeaponTypeId.RocketLauncher:
                    return _heroRocketLauncherRocketsPool.Get();

                case HeroWeaponTypeId.Mortar:
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