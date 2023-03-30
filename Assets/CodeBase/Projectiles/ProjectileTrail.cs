using System.Collections;
using CodeBase.StaticData.Projectiles;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class ProjectileTrail : MonoBehaviour
    {
        [SerializeField] private Transform _trailPosition;

        private float _startDelay;
        private float _endDelay;
        private ParticleSystem _particleSystem;
        private GameObject _trailVfx;

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
                if (_particleSystem == null)
                {
                    _particleSystem = _trailVfx.GetComponent<ParticleSystem>();
                }

                yield return new WaitForSeconds(_startDelay);
                _particleSystem?.Play(true);
            }
        }

        public void HideTrace() =>
            StartCoroutine(CoroutineHideTrace());

        private IEnumerator CoroutineHideTrace()
        {
            yield return new WaitForSeconds(_endDelay);
            Hide();
        }

        private void Hide() =>
            _particleSystem?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}