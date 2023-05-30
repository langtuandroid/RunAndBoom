using CodeBase.Projectiles.Movement;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class BaseProjectileHit : MonoBehaviour
    {
        [SerializeField] protected ProjectileTrail Trail;
        [SerializeField] protected ProjectileMovement Movement;

        private string[] _tags =
        {
            Constants.EnemyTag, Constants.ObstacleTag, Constants.DestructableTag, Constants.WallTag,
            Constants.GroundTag
        };

        protected bool IsTargetTag(string targetTag)
        {
            foreach (string tag in _tags)
                if (targetTag.Equals(tag))
                    return true;

            return false;
        }
    }
}