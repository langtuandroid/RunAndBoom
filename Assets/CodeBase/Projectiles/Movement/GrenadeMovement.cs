using System;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrenadeMovement : ProjectileMovement
    {
        private const float LaunchForce = 50f;

        private Rigidbody _rigidBody;

        public override event Action Stoped;

        private void Awake() =>
            _rigidBody = GetComponent<Rigidbody>();

        // public void Construct(float speed, float lifeTime) => 
        //     base.Construct(speed * 1f,lifeTime);

        public override void Launch()
        {
            _rigidBody.isKinematic = false;
            _rigidBody.AddForce(transform.forward * Speed * LaunchForce, ForceMode.Force);
            StartCoroutine(LaunchTime());
        }

        public override void Stop()
        {
            OffMove();
            _rigidBody.isKinematic = true;
            Stoped?.Invoke();
        }
    }
}