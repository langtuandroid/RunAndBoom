using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public abstract class ProjectileMovement : MonoBehaviour
    {
        private float _movementTimeLimit = 3f;
        private float _launchCounter = 0f;

        protected float Speed { get; private set; }

        protected bool IsMove { get; set; }

        public abstract event Action Stoped;

        public void Construct(float speed, float lifeTime)
        {
            Speed = speed;
            _movementTimeLimit = lifeTime;
            IsMove = false;
        }

        public abstract void Launch();
        public abstract void Stop();

        protected void OffMove() =>
            IsMove = false;

        protected IEnumerator LaunchTime()
        {
            _launchCounter = _movementTimeLimit;
            IsMove = true;

            while (_launchCounter > 0f)
            {
                _launchCounter -= Time.deltaTime;
                yield return null;
            }

            if (_launchCounter <= 0f)
            {
                Stop();
            }
        }
    }
}