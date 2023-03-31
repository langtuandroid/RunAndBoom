using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileHit : BaseProjectileHit
    {
        private void OnCollisionEnter(Collision collision)
        {
            string targetTag = collision.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                if (Trail != null)
                    Trail.HideTrace();

                Movement.Stop();
                HeroHealth heroHealth = collision.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }
    }
}