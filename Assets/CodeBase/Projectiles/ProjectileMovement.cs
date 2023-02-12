using UnityEngine;

namespace CodeBase.Projectiles
{
    public abstract class ProjectileMovement : MonoBehaviour
    {
        private Transform _parent;

        protected void Construct(Transform parent) =>
            _parent = parent;

        public abstract void Launch();
    }
}