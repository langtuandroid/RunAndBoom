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

        public void Construct(ProjectileTraceStaticData projectileTraceStaticData)
        {
            _traceVfxPrefab = projectileTraceStaticData.PrefabReference;
            _startDelay = projectileTraceStaticData.StartDelay;
            _endDelay = projectileTraceStaticData.EndDelay;
        }

        public void CreateTrace()
        {
            // StartCoroutine(CoroutineCreateTrace());
        }

        private IEnumerator CoroutineCreateTrace()
        {
            yield return new WaitForSeconds(_startDelay);
            _traceVfx = Instantiate(_traceVfxPrefab, _tracePosition);
        }

        public void DestroyTrace()
        {
            // StartCoroutine(CoroutineDestroyTrace());
        }

        private IEnumerator CoroutineDestroyTrace()
        {
            yield return new WaitForSeconds(_endDelay);
            Destroy(_traceVfx);
        }
    }
}