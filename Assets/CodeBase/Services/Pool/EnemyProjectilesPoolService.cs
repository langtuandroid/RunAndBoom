using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Constructor;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemies;
using CodeBase.StaticData.Projectiles;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Pool
{
    public class EnemyProjectilesPoolService : IEnemyProjectilesPoolService
    {
        private const int InitialCapacity = 4;
        private const string PistolBulletTag = "PistolBullet";
        private const string ShotTag = "Shot";
        private const string SniperRifleBulletTag = "SniperRifleBullet";
        private const string SMGBulletTag = "SMGBullet";
        private const string MGBulletTag = "MGBullet";

        private IAssets _assets;
        private IConstructorService _constructorService;
        private IStaticDataService _staticDataService;
        private Transform _root;
        private GameObject _gameObject;
        private ObjectPool<GameObject> _enemyPistolBulletsPool;
        private ObjectPool<GameObject> _enemyShotsPool;
        private ObjectPool<GameObject> _enemySniperRifleBulletsPool;
        private ObjectPool<GameObject> _enemySMGBulletsPool;
        private ObjectPool<GameObject> _enemyMGBulletsPool;
        private GameObject _projectile;
        private EnemyStaticData _enemyStaticData;
        private GameObject _pistolBulletPrefab;
        private GameObject _shotPrefab;

        public EnemyProjectilesPoolService(IAssets assets, IConstructorService constructorService,
            IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assets = assets;
            _constructorService = constructorService;
        }

        public async void GenerateObjects()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.EnemyProjectilesRoot);
            _gameObject = Object.Instantiate(root);
            _root = _gameObject.transform;
            _pistolBulletPrefab = await _assets.Instantiate(AssetAddresses.PistolBullet, _root);
            _shotPrefab = await _assets.Instantiate(AssetAddresses.Shot, _root);
            _pistolBulletPrefab.SetActive(false);
            _shotPrefab.SetActive(false);

            _enemyPistolBulletsPool = new ObjectPool<GameObject>(GetPistolBullet, GetFromPool, ReturnToBack,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _enemyShotsPool = new ObjectPool<GameObject>(GetShot, GetFromPool, ReturnToBack,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _enemySniperRifleBulletsPool = new ObjectPool<GameObject>(GetSniperRifleBullet, GetFromPool, ReturnToBack,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _enemySMGBulletsPool = new ObjectPool<GameObject>(GetSMGBullet, GetFromPool, ReturnToBack,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 5);

            _enemyMGBulletsPool = new ObjectPool<GameObject>(GetMGBullet, GetFromPool, ReturnToBack,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 5);
        }

        private GameObject GetPistolBullet()
        {
            _projectile = Object.Instantiate(_pistolBulletPrefab);
            _enemyStaticData = _staticDataService.ForEnemy(EnemyTypeId.WithPistol);
            _constructorService.ConstructEnemyProjectile(_projectile, _enemyStaticData.Damage,
                ProjectileTypeId.PistolBullet);
            return _projectile;
        }

        private GameObject GetShot()
        {
            _projectile = Object.Instantiate(_shotPrefab);
            _enemyStaticData = _staticDataService.ForEnemy(EnemyTypeId.WithShotgun);
            _constructorService.ConstructEnemyProjectile(_projectile, _enemyStaticData.Damage,
                ProjectileTypeId.Shot);
            return _projectile;
        }

        private GameObject GetSniperRifleBullet()
        {
            _projectile = Object.Instantiate(_pistolBulletPrefab);
            _enemyStaticData = _staticDataService.ForEnemy(EnemyTypeId.WithSniperRifle);
            _constructorService.ConstructEnemyProjectile(_projectile, _enemyStaticData.Damage,
                ProjectileTypeId.RifleBullet);
            return _projectile;
        }

        private GameObject GetSMGBullet()
        {
            _projectile = Object.Instantiate(_pistolBulletPrefab);
            _enemyStaticData = _staticDataService.ForEnemy(EnemyTypeId.WithSMG);
            _constructorService.ConstructEnemyProjectile(_projectile, _enemyStaticData.Damage,
                ProjectileTypeId.PistolBullet);
            return _projectile;
        }

        private GameObject GetMGBullet()
        {
            _projectile = Object.Instantiate(_pistolBulletPrefab);
            _enemyStaticData = _staticDataService.ForEnemy(EnemyTypeId.WithMG);
            _constructorService.ConstructEnemyProjectile(_projectile, _enemyStaticData.Damage,
                ProjectileTypeId.RifleBullet);
            return _projectile;
        }

        public GameObject GetFromPool(EnemyWeaponTypeId typeId)
        {
            switch (typeId)
            {
                case EnemyWeaponTypeId.Pistol:
                    _projectile = _enemyPistolBulletsPool.Get();
                    // _projectile.SetActive(true);
                    return _projectile;

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

        public void Return(GameObject pooledObject)
        {
            if (pooledObject.CompareTag(PistolBulletTag))
                _enemyPistolBulletsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(ShotTag))
                _enemyShotsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(SniperRifleBulletTag))
                _enemySniperRifleBulletsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(SMGBulletTag))
                _enemySMGBulletsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(MGBulletTag))
                _enemyMGBulletsPool.Release(pooledObject);
            else
                return;
        }

        private void ReturnToBack(GameObject pooledObject)
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