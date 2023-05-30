using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileHit : BaseProjectileHit
    {
        private float _damage;

        private void Awake() =>
            Tags = new[]
            {
                Constants.EnemyTag, Constants.HeroTag, Constants.ObstacleTag, Constants.DestructableTag,
                Constants.WallTag, Constants.GroundTag
            };

        public void Construct(float damage) =>
            _damage = damage;

        private void OnCollisionEnter(Collision collision)
        {
            string targetTag = collision.gameObject.tag;
            Debug.Log($"target tag: {targetTag}");

            if (IsTargetTag(targetTag))
            {
                if (Trail != null)
                    Trail.HideTrace();

                Movement.Stop();
                collision.gameObject.GetComponent<HeroHealth>()?.TakeDamage(_damage);
            }
        }
    }
}