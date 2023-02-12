using UnityEngine;

namespace CodeBase.Projectiles
{
    public abstract class ProjectileMovement : MonoBehaviour
    {
        protected Transform Parent;

        protected void Construct(Transform parent) =>
            Parent = parent;

        public abstract void Launch();
        public abstract void Stop();
    }
}