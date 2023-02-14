using System.Collections;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Projectiles
{
    public class ProjectileTrace : MonoBehaviour
    {
        [SerializeField] private Transform _tracePosition;

        private GameObject _traceVfxPrefab;
        private GameObject _traceVfx;
        private float _startDelay;
        private float _endDelay;
        private ParticleSystem _particleSystem;

        private void OnEnable() =>
            HideTrace();

        public void Construct(ProjectileTraceStaticData projectileTraceStaticData)
        {
            _traceVfxPrefab = projectileTraceStaticData.PrefabReference;
            _startDelay = projectileTraceStaticData.StartDelay;
            _endDelay = projectileTraceStaticData.EndDelay;
        }

        public void CreateTrace() =>
            StartCoroutine(CoroutineCreateTrace());

        private IEnumerator CoroutineCreateTrace()
        {
            if (_particleSystem == null)
            {
                var traceVfx = Instantiate(_traceVfxPrefab, _tracePosition.position, Quaternion.identity, _tracePosition);
                _particleSystem = traceVfx.GetComponent<ParticleSystem>();
            }

            yield return new WaitForSeconds(_startDelay);
            _particleSystem.Play(true);
        }

        private void HideTrace() =>
            _particleSystem?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        public void DestroyTrace() =>
            StartCoroutine(CoroutineDestroyTrace());

        private IEnumerator CoroutineDestroyTrace()
        {
            yield return new WaitForSeconds(_endDelay);
            HideTrace();
        }
    }
}