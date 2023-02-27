using System.Collections;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public abstract class ProjectileMovement : MonoBehaviour
    {
        protected Transform Parent;
        private float _movementTimeLimit = 3f;
        private float _launchCounter = 0f;

        protected void Construct(Transform parent, float lifeTime)
        {
            Parent = parent;
            _movementTimeLimit = lifeTime;
        }

        public abstract void Launch();
        public abstract void Stop();

        public IEnumerable LaunchTime()
        {
            _launchCounter = _movementTimeLimit;

            while (_launchCounter > 0f)
            {
                _launchCounter -= Time.deltaTime;
                yield return null;
            }

            if (_launchCounter <= 0f)
                Stop();
        }
    }
}