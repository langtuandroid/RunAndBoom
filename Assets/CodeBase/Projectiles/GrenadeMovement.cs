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

        public void Construct(float speed, Transform parent, float lifeTime)
        {
            _speed = speed * 1f;
            base.Construct(parent, lifeTime);
        }

        public override void Launch()
        {
            _rigidBody.isKinematic = false;
            _rigidBody.AddForce(Parent.forward * _speed * LaunchForce, ForceMode.Force);
        }

        public override void Stop()
        {
            _rigidBody.isKinematic = true;
            gameObject.SetActive(false);
        }
    }
}