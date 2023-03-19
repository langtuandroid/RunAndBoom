using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public class BulletMovement : ProjectileMovement
    {
        private float _speed;

        private void Update()
        {
            if (IsMove)
                transform.position += transform.forward * _speed * Time.deltaTime;
        }

        public void Construct(float speed, Transform parent, float lifeTime)
        {
            _speed = speed * 1f;
            base.Construct(parent, lifeTime);
        }

        public override void Launch()
        {
            StartCoroutine(LaunchTime());
            IsMove = true;
        }

        public override void Stop()
        {
            SetInactive();
            IsMove = false;
        }
    }
}