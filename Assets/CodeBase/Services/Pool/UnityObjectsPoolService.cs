using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Constructor;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.ShotVfxs;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Pool
{
    public class UnityObjectsPoolService : IObjectsPoolService
    {
        private const int InitialVfxCapacity = 15;
        private const int InitialEnemyProjectilesCapacity = 50;
        private const int InitialHeroProjectilesCapacity = 50;
        private const int AdditionalCount = 5;

        private IAssets _assets;
        private IConstructorService _constructorService;
        private IStaticDataService _staticDataService;
        private Transform _enemyProjectilesRoot;
        private Transform _heroProjectilesRoot;
        private Transform _shotVfxsRoot;
        private GameObject _gameObject;
        private ObjectPool<GameObject> _enemyPistolBulletsPool;
        private ObjectPool<GameObject> _enemyShotsPool;
        private ObjectPool<GameObject> _heroGrenadesPool;
        private ObjectPool<GameObject> _heroRpgRocketsPool;
        private ObjectPool<GameObject> _heroRocketLauncherRocketsPool;
        private ObjectPool<GameObject> _heroBombsPool;
        private int _defaultSize;
        private int _maxSize;
        private ObjectPool<GameObject> _grenadeMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _rpgMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _rocketLauncherMuzzleBlueFireVfxsPool;
        private ObjectPool<GameObject> _bombMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _bulletMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _shotMuzzleFireVfxsPool;

        public UnityObjectsPoolService(IAssets assets, IConstructorService constructorService,
            IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assets = assets;
            _constructorService = constructorService;
        }

        public void GenerateObjects() =>
            CreateRoots();

        public GameObject GetEnemyProjectile(string name)
        {
            throw new NotImplementedException();
        }

        public GameObject GetHeroProjectile(string name)
        {
            throw new NotImplementedException();
        }

        private async void CreateRoots()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.HeroProjectilesRoot);
            _gameObject = Object.Instantiate(root);
            _heroProjectilesRoot = _gameObject.transform;

            root = await _assets.Load<GameObject>(AssetAddresses.EnemyProjectilesRoot);
            _gameObject = Object.Instantiate(root);
            _enemyProjectilesRoot = _gameObject.transform;

            root = await _assets.Load<GameObject>(AssetAddresses.ShotVfxsRoot);
            _gameObject = Object.Instantiate(root);
            _shotVfxsRoot = _gameObject.transform;

            GenerateEnemyProjectiles();
            GenerateHeroProjectiles();
            GenerateShotVfxs();
        }

        private void GenerateEnemyProjectiles()
        {
            _enemyPistolBulletsPool = new ObjectPool<GameObject>(GetBullet, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialEnemyProjectilesCapacity, InitialEnemyProjectilesCapacity * 3);

            _enemyShotsPool = new ObjectPool<GameObject>(GetShot, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialEnemyProjectilesCapacity, InitialEnemyProjectilesCapacity * 3);
        }

        private GameObject GetBullet() =>
            _assets.Instantiate(AssetAddresses.PistolBullet, _enemyProjectilesRoot).Result;

        private GameObject GetShot() =>
            _assets.Instantiate(AssetAddresses.Shot, _enemyProjectilesRoot).Result;

        private void GenerateHeroProjectiles()
        {
            _heroGrenadesPool = new ObjectPool<GameObject>(GetGrenade, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialHeroProjectilesCapacity, InitialHeroProjectilesCapacity * 3);

            _heroRpgRocketsPool = new ObjectPool<GameObject>(GetRpgRocket, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialHeroProjectilesCapacity, InitialHeroProjectilesCapacity * 3);

            _heroRocketLauncherRocketsPool = new ObjectPool<GameObject>(GetRocketLauncherRocket, OnGetFromPool,
                OnReturnToPool,
                OnDestroyPooledObject, true, InitialHeroProjectilesCapacity, InitialHeroProjectilesCapacity * 3);

            _heroBombsPool = new ObjectPool<GameObject>(GetBomb, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialHeroProjectilesCapacity, InitialHeroProjectilesCapacity * 3);
        }

        private GameObject GetGrenade() =>
            _assets.Instantiate(AssetAddresses.Grenade, _heroProjectilesRoot).Result;

        private GameObject GetRpgRocket() =>
            _assets.Instantiate(AssetAddresses.RpgRocket, _heroProjectilesRoot).Result;

        private GameObject GetRocketLauncherRocket() =>
            _assets.Instantiate(AssetAddresses.RocketLauncherRocket, _heroProjectilesRoot).Result;

        private GameObject GetBomb() =>
            _assets.Instantiate(AssetAddresses.Bomb, _heroProjectilesRoot).Result;

        private void GenerateShotVfxs()
        {
            _grenadeMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetGrenadeMuzzleFire, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialVfxCapacity, InitialVfxCapacity * 3);

            _rpgMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetRpgMuzzleFire, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialVfxCapacity, InitialVfxCapacity * 3);

            _rocketLauncherMuzzleBlueFireVfxsPool = new ObjectPool<GameObject>(GetRocketLauncherMuzzleBlueFire,
                OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialVfxCapacity, InitialVfxCapacity * 3);

            _bombMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetBombMuzzleFire, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialVfxCapacity, InitialVfxCapacity * 3);

            _bulletMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetBulletMuzzleFire, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialVfxCapacity, InitialVfxCapacity * 3);

            _shotMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetShotMuzzleFire, OnGetFromPool, OnReturnToPool,
                OnDestroyPooledObject, true, InitialVfxCapacity, InitialVfxCapacity * 3);
        }

        private GameObject GetGrenadeMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.GrenadeMuzzleFire, _shotVfxsRoot).Result;

        private GameObject GetRpgMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.RpgMuzzleFire, _shotVfxsRoot).Result;

        private GameObject GetRocketLauncherMuzzleBlueFire() =>
            _assets.Instantiate(AssetAddresses.RocketLauncherMuzzleBlue, _shotVfxsRoot).Result;

        private GameObject GetBombMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.BombMuzzle, _shotVfxsRoot).Result;

        private GameObject GetBulletMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.BulletMuzzleFire, _shotVfxsRoot).Result;

        private GameObject GetShotMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.ShotMuzzleFire, _shotVfxsRoot).Result;

        public GameObject GetEnemyProjectile(EnemyWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case EnemyWeaponTypeId.Pistol:
                    return _enemyPistolBulletsPool.Get();

                case EnemyWeaponTypeId.Shotgun:
                    return _enemyShotsPool.Get();

                case EnemyWeaponTypeId.SniperRifle:
                    return _enemyPistolBulletsPool.Get();

                case EnemyWeaponTypeId.SMG:
                    return _enemyPistolBulletsPool.Get();

                case EnemyWeaponTypeId.MG:
                    return _enemyPistolBulletsPool.Get();
            }

            return null;
        }

        public GameObject GetHeroProjectile(HeroWeaponTypeId typeId)
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

        public GameObject GetShotVfx(ShotVfxTypeId typeId)
        {
            switch (typeId)
            {
                case ShotVfxTypeId.Bullet:
                    return _bulletMuzzleFireVfxsPool.Get();

                case ShotVfxTypeId.Shot:
                    return _shotMuzzleFireVfxsPool.Get();

                case ShotVfxTypeId.Grenade:
                    return _grenadeMuzzleFireVfxsPool.Get();

                case ShotVfxTypeId.RpgRocket:
                    return _rpgMuzzleFireVfxsPool.Get();

                case ShotVfxTypeId.RocketLauncherRocket:
                    return _rocketLauncherMuzzleBlueFireVfxsPool.Get();

                case ShotVfxTypeId.Bomb:
                    return _bombMuzzleFireVfxsPool.Get();
            }

            return null;
        }

        private void OnReturnToPool(GameObject pooledObject) =>
            pooledObject.SetActive(false);

        private void OnGetFromPool(GameObject pooledObject) =>
            pooledObject.SetActive(true);

        private void OnDestroyPooledObject(GameObject pooledObject) =>
            Object.Destroy(pooledObject);

        public void ReturnEnemyProjectile(GameObject gameObject)
        {
        }

        public void ReturnHeroProjectile(GameObject gameObject)
        {
        }

        public void ReturnShotVfx(GameObject gameObject)
        {
        }
    }
}