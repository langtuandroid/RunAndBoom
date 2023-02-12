using UnityEngine;

namespace CodeBase.Projectiles
{
    public class BulletMovement: ProjectileMovement
    {
        private CapsuleCollider _collider;
        private float _speed;
        private bool _launch = false;
        private float _colliderCooldown = 0.5f;
        private float _colliderCooldownTimer = 0f;

        private void Awake()
        {
            _collider = GetComponentInChildren<CapsuleCollider>();
            // _collider.enabled = true;
        }

        // private void OnEnable() =>
        //     _collider.enabled = false;
        //
        // private void OnDisable() =>
        //     _collider.enabled = false;

        private void Update()
        {
            if (_launch)
            {
                TimerUp();
                transform.Translate(transform.forward * _speed * Time.deltaTime);
            }

            // if (CheckColliderCooldownTimer())
            //     _collider.enabled = true;
        }

        private bool CheckColliderCooldownTimer() =>
            _colliderCooldownTimer >= _colliderCooldown;

        private void TimerUp() =>
            _colliderCooldownTimer += Time.deltaTime;
        
        public void Construct(float speed, Transform parent)
        {
            _speed = speed * 1f;
            base.Construct(parent);
        }

        public override void Launch() =>
            _launch = true;
    }
}