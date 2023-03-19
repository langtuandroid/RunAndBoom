using System.Collections;
using UnityEngine;

namespace CodeBase.Projectiles.Movement
{
    public abstract class ProjectileMovement : MonoBehaviour
    {
        public bool IsMove { get; protected set; }

        protected Transform Parent;

        private float _movementTimeLimit = 3f;
        private float _launchCounter = 0f;

        protected void Construct(Transform parent, float lifeTime)
        {
            Parent = parent;
            _movementTimeLimit = lifeTime;
            IsMove = false;
        }

        public abstract void Launch();
        public abstract void Stop();

        protected void SetInactive()
        {
            IsMove = false;
            gameObject.SetActive(false);
            gameObject.transform.SetParent(Parent);
        }

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