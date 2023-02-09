using System.Collections;
using CodeBase.Hero;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Projectile
{
    [RequireComponent(typeof(DestroyWithBlast))]
    public abstract class Projectile : MonoBehaviour
    {
        private const float LaunchForce = 50f;

        private IStaticDataService _staticDataService;
        private DestroyWithBlast _destroyWithBlast;
        private ProjectileTraceStaticData _projectileTraceStaticData;
        private GameObject _traceVfx;
        private GameObject _blastVfx;
        private Transform _tracePosition;
        private float _sphereRadius;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Environments") || collision.gameObject.CompareTag("Floor"))
            {
                Debug.Log("Blast!");
                var blastVfx = Instantiate(_blastVfx, transform.position, Quaternion.identity);
                StartCoroutine(DestroyBlast(blastVfx));

                _destroyWithBlast.DestroyAllAround(_sphereRadius);

                if (_projectileTraceStaticData.ProjectileTraceTypeId != ProjectileTraceTypeId.None)
                    StartCoroutine(DestroyTrace());

                gameObject.SetActive(false);
            }

            if (collision.gameObject.CompareTag("Hero"))
            {
                HeroHealth heroHealth = collision.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }

        protected void Construct(GameObject blastVfx, ProjectileTraceStaticData projectileTraceStaticData, float blastRadius)
        {
            _blastVfx = blastVfx;
            _traceVfx = projectileTraceStaticData.PrefabReference;
            _projectileTraceStaticData = projectileTraceStaticData;
            _sphereRadius = blastRadius;
            _destroyWithBlast = GetComponent<DestroyWithBlast>();
        }

        public abstract void Launch();


        public void CreateTrace()
        {
            if (_projectileTraceStaticData.ProjectileTraceTypeId != ProjectileTraceTypeId.None)
                StartCoroutine(CoroutineCreateTrace());
        }

        private IEnumerator CoroutineCreateTrace()
        {
            yield return new WaitForSeconds(_projectileTraceStaticData.StartDelay);
            _traceVfx = Instantiate(_traceVfx, _tracePosition);
        }

        private IEnumerator DestroyBlast(GameObject blastVfx)
        {
            yield return new WaitForSeconds(2f);
            Destroy(blastVfx);
        }

        private IEnumerator DestroyTrace()
        {
            yield return new WaitForSeconds(_projectileTraceStaticData.EndDelay);
            Destroy(_traceVfx);
        }
    }
}