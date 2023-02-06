using System.Collections;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Projectile
{
    [RequireComponent(typeof(DestroyWithBlast)
        , typeof(Rigidbody)
    )]
    public class Projectile : MonoBehaviour
    {
        private DestroyWithBlast _destroyWithBlast;
        private ProjectileTraceStaticData _projectileTraceStaticData;
        private GameObject _traceVfx;
        private GameObject _blastVfx;
        private Transform _tracePosition;
        private Rigidbody _rigidBody;

        private void Awake()
        {
            _destroyWithBlast = GetComponent<DestroyWithBlast>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        public void SetSpeed(Vector3 speed) =>
            _rigidBody.velocity = speed;


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

        public void Construct(GameObject blastVfx, ProjectileTraceStaticData projectileTraceStaticData)
        {
            _blastVfx = blastVfx;
            _projectileTraceStaticData = projectileTraceStaticData;
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
            Instantiate(_blastVfx);

            _destroyWithBlast.DestroyAllAround();

            if (_projectileTraceStaticData.ProjectileTraceTypeId != ProjectileTraceTypeId.None)
                StartCoroutine(DestroyTrace());
        }

        private IEnumerator DestroyTrace()
        {
            yield return new WaitForSeconds(_projectileTraceStaticData.EndDelay);
            Destroy(_traceVfx);
            Destroy(this);
        }
    }
}