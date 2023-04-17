using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class BombMovement : ProjectileMovement
    {
        [HideInInspector] public float Power { get; private set; }
        [HideInInspector] public Rigidbody Rigidbody { get; private set; }

        private bool _rotate = false;
        private Vector3 _targetPosition;
        private Vector3 _speed = Vector3.zero;

        public override event Action Stoped;

        private void Awake()
        {
            Power = 500f;
            Rigidbody = GetComponent<Rigidbody>();
        }

        // public void Construct(float speed, float lifeTime) => 
        //     base.Construct(speed * 1f,lifeTime);

        public void SetSpeed(Vector3 speed)
        {
            _speed = speed;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public override void Launch()
        {
            StartCoroutine(LaunchTime());

            _rotate = true;
            Rigidbody.isKinematic = false;
            Vector3 aim = _targetPosition - transform.position;
            float lenght = Vector3.Distance(_targetPosition, transform.position);
            float time = lenght / Speed;
            float antiGravity = -Physics.gravity.y * time / 2;
            float deltaY = (_targetPosition.y - transform.position.y) / time;
            Vector3 bombSpeed = aim.normalized * Speed;
            bombSpeed.y = antiGravity + deltaY;
            Rigidbody.velocity = bombSpeed;
            transform.forward = _targetPosition;

            // if (_rotate)
            //     transform.DORotate(new Vector3(120, 0, 0), 2f, RotateMode.Fast)
            //         .SetDelay(0.1f);
        }

        public override void Stop()
        {
            OffMove();
            _rotate = false;
            Rigidbody.isKinematic = true;
            Stoped?.Invoke();
        }
    }
}