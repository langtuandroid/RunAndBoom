using System.Collections;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class ProjectileTrail : MonoBehaviour
    {
        [SerializeField] private Transform _trailPosition;
        [SerializeField] private GameObject _trailVfx;

        private float _startDelay;
        private float _endDelay;
        private ParticleSystem _particleSystem;

        private void OnEnable() =>
            Hide();

        public void Construct(TrailStaticData trailStaticData)
        {
            _startDelay = trailStaticData.StartDelay;
            _endDelay = trailStaticData.EndDelay;
            CreateTrailVfx(trailStaticData.Prefab);
        }

        private void CreateTrailVfx(GameObject prefab)
        {
            if (_trailVfx == null)
                _trailVfx = Instantiate(prefab, _trailPosition.position, Quaternion.identity, _trailPosition);
        }


        public void ShowTrail() =>
            StartCoroutine(CoroutineShowTrace());

        private IEnumerator CoroutineShowTrace()
        {
            if (_trailVfx != null)
            {
                // if (_particleSystem == null) 
                //     _particleSystem = _trailVfx.GetComponent<ParticleSystem>();

                yield return new WaitForSeconds(_startDelay);
                _trailVfx.SetActive(true);
                // _particleSystem?.Play(true);
            }
        }

        public void HideTrace() =>
            StartCoroutine(CoroutineHideTrace());

        private IEnumerator CoroutineHideTrace()
        {
            yield return new WaitForSeconds(_endDelay);
            Hide();
        }

        private void Hide()
        {
            if (_trailVfx != null)
                _trailVfx.SetActive(false);
            // _particleSystem?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}