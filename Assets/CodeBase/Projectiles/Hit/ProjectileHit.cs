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
                Debug.Log("OnTriggerEnter");
                Trace.DestroyTrace();
                Movement.Stop();
                HeroHealth heroHealth = other.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            string targetTag = collision.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                Debug.Log("OnCollisionEnter");
                Trace.DestroyTrace();
                Movement.Stop();
                HeroHealth heroHealth = collision.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }
    }
}