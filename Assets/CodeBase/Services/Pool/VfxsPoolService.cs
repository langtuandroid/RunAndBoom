using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Pool
{
    public class VfxsPoolService : IVfxsPoolService
    {
        private const int InitialCapacity = 1;
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
        private GameObject _grenadeMuzzleFirePrefab;
        private GameObject _rpgMuzzleFirePrefab;
        private GameObject _rocketLauncherMuzzleBluePrefab;
        private GameObject _bombMuzzlePrefab;
        private GameObject _bulletMuzzleFirePrefab;
        private GameObject _shotMuzzleFirePrefab;

        public VfxsPoolService(IAssets assets)
        {
            _assets = assets;
        }

        public async void GenerateObjects()
        {
            Debug.Log("VfxsPoolService GenerateObjects");
            if (_grenadeMuzzleFireVfxsPool != null)
                _grenadeMuzzleFireVfxsPool.Dispose();

            if (_rpgMuzzleFireVfxsPool != null)
                _rpgMuzzleFireVfxsPool.Dispose();

            if (_rocketLauncherMuzzleBlueFireVfxsPool != null)
                _rocketLauncherMuzzleBlueFireVfxsPool.Dispose();

            if (_bombMuzzleFireVfxsPool != null)
                _bombMuzzleFireVfxsPool.Dispose();

            if (_bulletMuzzleFireVfxsPool != null)
                _bulletMuzzleFireVfxsPool.Dispose();

            if (_shotMuzzleFireVfxsPool != null)
                _shotMuzzleFireVfxsPool.Dispose();

            GameObject root = await _assets.Load<GameObject>(AssetAddresses.ShotVfxsRoot);
            _gameObject = Object.Instantiate(root);
            _root = _gameObject.transform;
            _grenadeMuzzleFirePrefab = await _assets.Instantiate(AssetAddresses.GrenadeMuzzleFire, _root);
            _rpgMuzzleFirePrefab = await _assets.Instantiate(AssetAddresses.RpgMuzzleFire, _root);
            _rocketLauncherMuzzleBluePrefab = await
                _assets.Instantiate(AssetAddresses.RocketLauncherMuzzleBlue, _root);
            _bombMuzzlePrefab = await _assets.Instantiate(AssetAddresses.BombMuzzle, _root);
            _bulletMuzzleFirePrefab = await _assets.Instantiate(AssetAddresses.BulletMuzzleFire, _root);
            _shotMuzzleFirePrefab = await _assets.Instantiate(AssetAddresses.ShotMuzzleFire, _root);
            _grenadeMuzzleFirePrefab.SetActive(false);
            _rpgMuzzleFirePrefab.SetActive(false);
            _rocketLauncherMuzzleBluePrefab.SetActive(false);
            _bombMuzzlePrefab.SetActive(false);
            _bulletMuzzleFirePrefab.SetActive(false);
            _shotMuzzleFirePrefab.SetActive(false);

            _grenadeMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetGrenadeMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _rpgMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetRpgMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _rocketLauncherMuzzleBlueFireVfxsPool = new ObjectPool<GameObject>(GetRocketLauncherMuzzleBlueFire,
                GetFromPool, ReturnToPool, DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _bombMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetBombMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _bulletMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetBulletMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _shotMuzzleFireVfxsPool = new ObjectPool<GameObject>(GetShotMuzzleFire, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);
        }

        private GameObject GetGrenadeMuzzleFire() =>
            Object.Instantiate(_grenadeMuzzleFirePrefab);

        private GameObject GetRpgMuzzleFire() =>
            Object.Instantiate(_rpgMuzzleFirePrefab);

        private GameObject GetRocketLauncherMuzzleBlueFire() =>
            Object.Instantiate(_rocketLauncherMuzzleBluePrefab);

        private GameObject GetBombMuzzleFire() =>
            Object.Instantiate(_bombMuzzlePrefab);

        private GameObject GetBulletMuzzleFire() =>
            Object.Instantiate(_bulletMuzzleFirePrefab);

        private GameObject GetShotMuzzleFire() =>
            Object.Instantiate(_shotMuzzleFirePrefab);

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

        public void Return(GameObject pooledObject)
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

        private void DestroyPooledObject(GameObject pooledObject)
        {
            Object.Destroy(pooledObject);
        }
    }
}