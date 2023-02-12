using UnityEngine;

namespace CodeBase.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrenadeMovement : ProjectileMovement
    {
        private const float LaunchForce = 50f;

        private Rigidbody _rigidBody;
        private float _speed;

        private void Awake() =>
            _rigidBody = GetComponent<Rigidbody>();

        public void Construct(float speed, Transform parent)
        {
            _speed = speed * 1f;
            base.Construct(parent);
        }

        public override void Launch()
        {
            _rigidBody.AddForce(Vector3.forward * _speed * LaunchForce, ForceMode.Force);
            Debug.Log($"velocity {_rigidBody.velocity}");
        }
    }
}