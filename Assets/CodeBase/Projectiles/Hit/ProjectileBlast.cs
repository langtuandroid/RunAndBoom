using System.Collections;
using UnityEngine;

namespace CodeBase.Projectiles.Hit
{
    public class ProjectileBlast : BaseProjectileHit
    {
        [SerializeField] private DestroyWithBlast _destroyWithBlast;

        private const float BlastDuration = 2f;

        private GameObject _prefab;
        private float _sphereRadius;
        private ParticleSystem _particleSystem;
        private GameObject _blastVfx;
        private float _damage;
        private CapsuleCollider _hitCollider;

        private void Awake() =>
            _hitCollider = GetComponent<CapsuleCollider>();

        public void OffCollider() =>
            _hitCollider.enabled = false;

        public void OnCollider() =>
            _hitCollider.enabled = transform;

        private void OnEnable() =>
            HideBlast();

        private void OnTriggerEnter(Collider other)
        {
            string targetTag = other.gameObject.tag;

            if (IsTargetTag(targetTag))
            {
                if (_prefab != null)
                {
                    ShowBlast();
                    StartCoroutine(DestroyBlast());
                    _destroyWithBlast.HitAllAround(_sphereRadius, _damage);
                }

                Trail?.HideTrace();
                Movement.Stop();
            }
        }

        public void Construct(GameObject prefab, float radius, float damage)
        {
            _prefab = prefab;
            _sphereRadius = radius;
            _damage = damage;
        }

        private void ShowBlast()
        {
            if (_particleSystem == null)
            {
                _blastVfx = Instantiate(_prefab, transform.position, Quaternion.identity, null);
                _particleSystem = _blastVfx.GetComponent<ParticleSystem>();
            }
            else
            {
                _blastVfx.transform.position = transform.position;
            }

            _particleSystem.Play(true);
        }

        private void HideBlast()
        {
            if (_particleSystem != null)
                _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private IEnumerator DestroyBlast()
        {
            yield return new WaitForSeconds(BlastDuration);
            HideBlast();
        }
    }
}