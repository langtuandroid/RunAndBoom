using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Pool
{
    public class VfxsPoolService : IVfxsPoolService
    {
        private const int InitialCapacity = 4;
        private const string BulletMuzzleFireVfxTag = "BulletMuzzleFireVfx";
        private const string ShotMuzzleFireVfxTag = "ShotMuzzleFireVfx";
        private const string GrenadeMuzzleFireVfxTag = "GrenadeMuzzleFireVfx";
        private const string RpgMuzzleFireVfxTag = "RpgMuzzleFireVfx";
        private const string RocketLauncherMuzzleBlueFireVfxTag = "RocketLauncherMuzzleBlueFireVfx";
        private const string BombMuzzleFireVfxTag = "BombMuzzleFireVfx";

        private IAssets _assets;
        private Transform _root;
        private GameObject _gameObject;
        private ObjectPool<GameObject> _grenadeMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _rpgMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _rocketLauncherMuzzleBlueFireVfxsPool;
        private ObjectPool<GameObject> _bombMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _bulletMuzzleFireVfxsPool;
        private ObjectPool<GameObject> _shotMuzzleFireVfxsPool;

        public VfxsPoolService(IAssets assets) =>
            _assets = assets;

        public async void GenerateObjects()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.HeroProjectilesRoot);
            _gameObject = Object.Instantiate(root);
            _root = _gameObject.transform;

            _grenadeMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetGrenadeMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _rpgMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetRpgMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _rocketLauncherMuzzleBlueFireVfxsPool = new ObjectPool<GameObject>(GetRocketLauncherMuzzleBlueFire,
                GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _bombMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetBombMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _bulletMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetBulletMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _shotMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetShotMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);
        }

        private GameObject GetGrenadeMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.GrenadeMuzzleFire, _root).Result;

        private GameObject GetRpgMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.RpgMuzzleFire, _root).Result;

        private GameObject GetRocketLauncherMuzzleBlueFire() =>
            _assets.Instantiate(AssetAddresses.RocketLauncherMuzzleBlue, _root).Result;

        private GameObject GetBombMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.BombMuzzle, _root).Result;

        private GameObject GetBulletMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.BulletMuzzleFire, _root).Result;

        private GameObject GetShotMuzzleFire() =>
            _assets.Instantiate(AssetAddresses.ShotMuzzleFire, _root).Result;

        public GameObject GetFromPool(ShotVfxTypeId typeId)
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

        public void ReturnToPool(GameObject pooledObject)
        {
            if (pooledObject.CompareTag(BulletMuzzleFireVfxTag))
                _bulletMuzzleFireVfxsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(ShotMuzzleFireVfxTag))
                _shotMuzzleFireVfxsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(GrenadeMuzzleFireVfxTag))
                _grenadeMuzzleFireVfxsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(RpgMuzzleFireVfxTag))
                _rpgMuzzleFireVfxsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(RocketLauncherMuzzleBlueFireVfxTag))
                _rocketLauncherMuzzleBlueFireVfxsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(BombMuzzleFireVfxTag))
                _bombMuzzleFireVfxsPool.Release(pooledObject);
            else
                return;

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