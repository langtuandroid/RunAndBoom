using System;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public class ShotMovement : ProjectileMovement
    {
        private float _speed;

        public override event Action Stoped;

        private void Update()
        {
            if (IsMove)
                transform.position += transform.forward * _speed * Time.deltaTime;
        }

        public void Construct(float speed, float lifeTime)
        {
            _speed = speed * 1f;
            base.Construct(lifeTime);
        }

        public override void Launch()
        {
            StartCoroutine(LaunchTime());
            IsMove = true;
        }

        public override void Stop()
        {
            OffMove();
            Stoped?.Invoke();
        }
    }
}