using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileHit : BaseProjectileHit
    {
        private float _damage;

        public void Construct(float damage) =>
            _damage = damage;

        private void OnCollisionEnter(Collision collision)
        {
            string targetTag = collision.gameObject.tag;

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