using System.Collections;
using CodeBase.Services;
using CodeBase.Services.Pool;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class ShotVfxsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _shotVfx;

        private IObjectsPoolService _objectsPoolService;
        private float _shotVfxLifetime;
        private int _index;
        private Transform _root;
        private ShotVfxTypeId _shotVfxTypeId;
        private WaitForSeconds _coroutineLaunchShotVfx;

        public void Construct(float shotVfxLifetime, ShotVfxTypeId shotVfxTypeId, Transform root)
        {
            _shotVfxTypeId = shotVfxTypeId;
            _objectsPoolService = AllServices.Container.Single<IObjectsPoolService>();
            _shotVfxLifetime = shotVfxLifetime;
            _root = root;

            if (_coroutineLaunchShotVfx == null)
                _coroutineLaunchShotVfx = new WaitForSeconds(_shotVfxLifetime);
        }

        public async void ShowShotVfx(Transform muzzleTransform)
        {
            _shotVfx = await _objectsPoolService.GetShotVfx(_shotVfxTypeId);
            _shotVfx.transform.SetParent(_root);
            SetShotVfx(_shotVfx, muzzleTransform);
            StartCoroutine(CoroutineLaunchShotVfx());
        }

        private void SetShotVfx(GameObject shotVfx, Transform muzzleTransform)
        {
            shotVfx.transform.position = muzzleTransform.position;
            shotVfx.transform.rotation = muzzleTransform.rotation;
        }

        private IEnumerator CoroutineLaunchShotVfx()
        {
            _shotVfx.SetActive(true);
            yield return _coroutineLaunchShotVfx;
            ReturnShotVfx();
        }

        public void ReturnShotVfx()
        {
            if (_objectsPoolService == null || _shotVfx == null)
                return;

            _objectsPoolService.ReturnShotVfx(_shotVfxTypeId.ToString(), _shotVfx);
            _shotVfx = null;
        }
    }
}