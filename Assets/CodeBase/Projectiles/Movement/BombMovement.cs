using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class BombMovement : ProjectileMovement
    {
        public float Power = 50f;

        private Rigidbody _rigidBody;
        private bool _rotate = false;
        private Vector3 _targetPosition;
        private Vector3 _speed = Vector3.zero;

        public override event Action Stoped;

        private void Awake() =>
            _rigidBody = GetComponent<Rigidbody>();

        // public void Construct(float speed, float lifeTime) => 
        //     base.Construct(speed * 1f,lifeTime);

        public void SetSpeed(Vector3 speed) =>
            _speed = speed;

        public void SetTargetPosition(Vector3 targetPosition) =>
            _targetPosition = targetPosition;

        public override void Launch()
        {
            StartCoroutine(LaunchTime());

            _rotate = true;
            // _rigidBody.isKinematic = false;
            // Vector3 aim = _targetPosition - transform.position;
            // float lenght = Vector3.Distance(_targetPosition, transform.position);
            // float time = lenght / Speed;
            // float antiGravity = -Physics.gravity.y * time / 2;
            // float deltaY = (_targetPosition.y - transform.position.y) / time;
            // Vector3 bombSpeed = aim.normalized * Speed;
            // bombSpeed.y = antiGravity + deltaY;
            // _rigidBody.velocity = bombSpeed;
            // transform.forward = _targetPosition;
            // _rigidBody.velocity = transform.forward * Power;

            // float lenght = Vector3.Distance(_targetPosition, transform.position);
            // float time = lenght / Speed;

            _rigidBody.AddForce(_speed, ForceMode.VelocityChange);

            if (_rotate)
                transform.DORotate(new Vector3(120, 0, 0), 2f, RotateMode.Fast)
                    .SetDelay(0.1f);
        }

        public override void Stop()
        {
            OffMove();
            _rotate = false;
            // _rigidBody.isKinematic = true;
            Stoped?.Invoke();
        }
    }
}