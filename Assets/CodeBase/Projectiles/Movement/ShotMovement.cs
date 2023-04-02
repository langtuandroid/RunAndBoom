using System;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public class ShotMovement : ProjectileMovement
    {
        public override event Action Stoped;

        private void Update()
        {
            if (IsMove)
                transform.position += transform.forward * Speed * Time.deltaTime;
        }

        // public void Construct(float speed, float lifeTime)
        // {
        //     base.Construct(speed * 1f, lifeTime);
        // }

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