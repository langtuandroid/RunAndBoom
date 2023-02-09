using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grenade : Projectile
    {
        private const float LaunchForce = 50f;

        private Rigidbody _rigidBody;
        private float _speed;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        public void Construct(GameObject blastVfx, ProjectileTraceStaticData projectileTraceStaticData, float speed, float blastRadius)
        {
            _speed = speed;
            base.Construct(blastVfx, projectileTraceStaticData, blastRadius);
        }

        public override void Launch()
        {
            _rigidBody.AddForce(Vector3.forward * _speed * LaunchForce, ForceMode.Force);
            Debug.Log($"velocity {_rigidBody.velocity}");
        }
    }
}