using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileHit : BaseProjectileHit
    {
        private void OnTriggerEnter(Collider other)
        {
            string targetTag = other.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                Trace.DestroyTrace();
                Movement.Stop();
                HeroHealth heroHealth = other.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }
    }
}