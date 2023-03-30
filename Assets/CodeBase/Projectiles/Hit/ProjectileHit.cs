using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileHit : BaseProjectileHit
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");
            string targetTag = other.gameObject.tag;
            // Debug.Log($"targetTag {targetTag}");
            Debug.Log($"gameObject name {other.gameObject.name}");

            if (IsTargetTag(targetTag))
            {
                Trail.HideTrace();
                Movement.Stop();
                HeroHealth heroHealth = other.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("OnCollisionEnter");
            string targetTag = collision.gameObject.tag;
            // Debug.Log($"targetTag {targetTag}");
            Debug.Log($"gameObject name {collision.gameObject.name}");

            if (IsTargetTag(targetTag))
            {
                Trail.HideTrace();
                Movement.Stop();
                HeroHealth heroHealth = collision.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
                // hit.transform.gameObject.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }
    }
}