using System.Collections;
using CodeBase.Services;
using CodeBase.Services.Pool;
using CodeBase.StaticData.ShotVfxs;
using UnityEngine;

namespace CodeBase.Weapons
{
    public class ShotVfxsContainer : MonoBehaviour
    {
        private IPoolService _poolService;
        private float _shotVfxLifetime;
        private int _index;
        private Transform _root;
        private ShotVfxTypeId _shotVfxTypeId;

        public void Construct(float shotVfxLifetime, ShotVfxTypeId shotVfxTypeId, Transform root)
        {
            _shotVfxTypeId = shotVfxTypeId;
            _poolService = AllServices.Container.Single<IPoolService>();
            _shotVfxLifetime = shotVfxLifetime;
            _root = root;
        }

        public void ShowShotVfx(Transform muzzleTransform)
        {
            GameObject shotVfx = _poolService.GetShotVfx(_shotVfxTypeId);
            shotVfx.transform.SetParent(_root);
            SetShotVfx(shotVfx, muzzleTransform);
            StartCoroutine(CoroutineLaunchShotVfx(shotVfx));
        }

        private void SetShotVfx(GameObject shotVfx, Transform muzzleTransform)
        {
            shotVfx.transform.position = muzzleTransform.position;
            shotVfx.transform.rotation = muzzleTransform.rotation;
        }

        private IEnumerator CoroutineLaunchShotVfx(GameObject shotVfx)
        {
            shotVfx.SetActive(true);
            yield return new WaitForSeconds(_shotVfxLifetime);
            shotVfx.SetActive(false);
        }
    }
}