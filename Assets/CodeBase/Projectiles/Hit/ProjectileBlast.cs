using System.Collections;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileBlast : BaseProjectileHit
    {
        [SerializeField] private DestroyWithBlast _destroyWithBlast;

        private const float BlastDuration = 2f;

        private GameObject _blastVfxPrefab;
        private float _sphereRadius;
        private ParticleSystem _particleSystem;
        private GameObject _blastVfx;

        private void OnEnable() =>
            HideBlast();

        private void OnTriggerEnter(Collider other)
        {
            string targetTag = other.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                if (_blastVfxPrefab != null)
                {
                    ShowBlast();
                    StartCoroutine(DestroyBlast());
                    // _destroyWithBlast.DestroyAllAround(_sphereRadius);
                }

                Trace.DestroyTrace();
                Movement.Stop();
            }
        }

        public void Construct(GameObject blastVfxPrefab, float blastRadius)
        {
            _blastVfxPrefab = blastVfxPrefab;
            _sphereRadius = blastRadius;
        }

        private void ShowBlast()
        {
            Debug.Log("Blast!");
            if (_particleSystem == null)
            {
                _blastVfx = Instantiate(_blastVfxPrefab, transform.position, Quaternion.identity, null);
                _particleSystem = _blastVfx.GetComponent<ParticleSystem>();
            }
            else
            {
                _blastVfx.transform.position = transform.position;
            }

            _particleSystem.Play(true);
        }

        private void HideBlast() =>
            _particleSystem?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        private IEnumerator DestroyBlast()
        {
            yield return new WaitForSeconds(BlastDuration);
            HideBlast();
        }
    }
}