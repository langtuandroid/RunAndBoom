using CodeBase.Projectiles.Movement;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class BaseProjectileHit : MonoBehaviour
    {
        [SerializeField] protected ProjectileTrace Trace;
        [SerializeField] protected ProjectileMovement Movement;
        [SerializeField] protected string[] Tags;

        protected bool IsTargetTag(string targetTag)
        {
            foreach (string tag in Tags)
                if (targetTag.Equals(tag))
                    return true;

            return false;
        }
    }
}