using DG.Tweening;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class BombMovement : ProjectileMovement
    {
        private Rigidbody _rigidBody;
        private float _speed;
        private bool _rotate = false;
        private Vector3 _targetPosition;

        private void Awake() =>
            _rigidBody = GetComponent<Rigidbody>();

        public void Construct(float speed, Transform parent, float lifeTime)
        {
            _speed = speed * 1f;
            base.Construct(parent, lifeTime);
        }

        public void SetTargetPosition(Vector3 targetPosition) =>
            _targetPosition = targetPosition;

        public override void Launch()
        {
            StartCoroutine(LaunchTime());

            _rotate = true;
            _rigidBody.isKinematic = false;
            Vector3 aim = _targetPosition - transform.position;
            float lenght = Vector3.Distance(_targetPosition, transform.position);
            float time = lenght / _speed;
            float antiGravity = -Physics.gravity.y * time / 2;
            float deltaY = (_targetPosition.y - transform.position.y) / time;
            Vector3 bombSpeed = aim.normalized * _speed;
            bombSpeed.y = antiGravity + deltaY;
            _rigidBody.velocity = bombSpeed;
            transform.forward = _targetPosition;

            transform.DORotate(new Vector3(120, 0, 0), time, RotateMode.Fast)
                .SetDelay(0.1f);
        }

        public override void Stop()
        {
            _rotate = false;
            _rigidBody.isKinematic = true;
            gameObject.SetActive(false);
        }
    }
}