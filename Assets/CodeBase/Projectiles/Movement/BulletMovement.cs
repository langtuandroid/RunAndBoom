using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public class BulletMovement : ProjectileMovement
    {
        private float _speed;
        private bool _move = false;

        private void Update()
        {
            if (_move)
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
            _move = true;
        }

        public override void Stop()
        {
            _move = false;
            gameObject.SetActive(false);
        }
    }
}