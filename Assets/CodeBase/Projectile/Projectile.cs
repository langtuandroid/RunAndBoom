using System.Collections;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Projectile
{
    [RequireComponent(typeof(DestroyWithBlast), typeof(Rigidbody)
    )]
    public class Projectile : MonoBehaviour
    {
        private const float LaunchForce = 50f;
        private DestroyWithBlast _destroyWithBlast;
        private ProjectileTraceStaticData _projectileTraceStaticData;
        private GameObject _traceVfx;
        private GameObject _blastVfx;
        private Transform _tracePosition;
        private Rigidbody _rigidBody;
        private float _speed;
        private float _sphereRadius;

        private void Awake()
        {
            _destroyWithBlast = GetComponent<DestroyWithBlast>();
            _rigidBody = GetComponent<Rigidbody>();
        }


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

        public void Construct(GameObject blastVfx, ProjectileTraceStaticData projectileTraceStaticData, float speed, float sphereRadius)
        {
            _blastVfx = blastVfx;
            _projectileTraceStaticData = projectileTraceStaticData;
            _speed = speed;
            _sphereRadius = sphereRadius;
        }

        public void Launch()
        {
            _rigidBody.AddForce(Vector3.forward * _speed * LaunchForce, ForceMode.Force);
            Debug.Log($"velocity {_rigidBody.velocity}");
        }

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