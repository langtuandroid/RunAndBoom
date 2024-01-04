using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData.Hits;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Services.Pool
{
    public class BlastsPoolService : IBlastsPoolService
    {
        private const int InitialCapacity = 1;
        private const string GrenadeBlastTag = "GrenadeBlast";
        private const string RpgRocketBlastTag = "RpgRocketBlast";
        private const string RocketLauncherRocketBlastTag = "RocketLauncherRocketBlast";
        private const string BombBlastTag = "BombBlast";

        private IAssets _assets;
        private Transform _root;
        private GameObject _gameObject;
        private ObjectPool<GameObject> _grenadeBlastsPool;
        private ObjectPool<GameObject> _rpgRocketBlastsPool;
        private ObjectPool<GameObject> _rocketLauncherRocketBlastsPool;
        private ObjectPool<GameObject> _bombBlastsPool;
        private GameObject _grenadeBlastPrefab;
        private GameObject _rpgRocketBlastPrefab;
        private GameObject _rocketLauncherRocketBlastPrefab;
        private GameObject _bombBlastPrefab;

        public BlastsPoolService(IAssets assets)
        {
            _assets = assets;
        }

        public async void GenerateObjects()
        {
            GameObject root = await _assets.Load<GameObject>(AssetAddresses.BlastsVfxsRoot);
            _gameObject = Object.Instantiate(root);
            _root = _gameObject.transform;
            _grenadeBlastPrefab = await _assets.Instantiate(AssetAddresses.GrenadeBlast, _root);
            _rpgRocketBlastPrefab = await _assets.Instantiate(AssetAddresses.RpgRocketBlast, _root);
            _rocketLauncherRocketBlastPrefab = await
                _assets.Instantiate(AssetAddresses.RocketLauncherRocketBlast, _root);
            _bombBlastPrefab = await _assets.Instantiate(AssetAddresses.BombBlast, _root);
            _grenadeBlastPrefab.SetActive(false);
            _rpgRocketBlastPrefab.SetActive(false);
            _rocketLauncherRocketBlastPrefab.SetActive(false);
            _bombBlastPrefab.SetActive(false);

            _grenadeBlastsPool = new ObjectPool<GameObject>(GetGrenadeBlast, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _rpgRocketBlastsPool = new ObjectPool<GameObject>(GetRpgRocketBlast, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _rocketLauncherRocketBlastsPool = new ObjectPool<GameObject>(GetRocketLauncherRocketBlast,
                GetFromPool, ReturnToPool, DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);

            _bombBlastsPool = new ObjectPool<GameObject>(GetBombBlast, GetFromPool, ReturnToPool,
                DestroyPooledObject, true, InitialCapacity, InitialCapacity * 3);
        }

        private GameObject GetGrenadeBlast() =>
            Object.Instantiate(_grenadeBlastPrefab, _root);

        private GameObject GetRpgRocketBlast() =>
            Object.Instantiate(_rpgRocketBlastPrefab, _root);

        private GameObject GetRocketLauncherRocketBlast() =>
            Object.Instantiate(_rocketLauncherRocketBlastPrefab, _root);

        private GameObject GetBombBlast() =>
            Object.Instantiate(_bombBlastPrefab, _root);


        public GameObject GetFromPool(BlastTypeId typeId)
        {
            switch (typeId)
            {
                case BlastTypeId.Grenade:
                    return _grenadeBlastsPool.Get();

                case BlastTypeId.RpgRocket:
                    return _rpgRocketBlastsPool.Get();

                case BlastTypeId.RocketLauncherRocket:
                    return _rocketLauncherRocketBlastsPool.Get();

                case BlastTypeId.Bomb:
                    return _bombBlastsPool.Get();

                case BlastTypeId.None:
                    return null;
            }

            return null;
        }

        public void Return(GameObject pooledObject)
        {
            if (pooledObject.CompareTag(GrenadeBlastTag))
                _grenadeBlastsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(RpgRocketBlastTag))
                _rpgRocketBlastsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(RocketLauncherRocketBlastTag))
                _rocketLauncherRocketBlastsPool.Release(pooledObject);
            else if (pooledObject.CompareTag(BombBlastTag))
                _bombBlastsPool.Release(pooledObject);
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