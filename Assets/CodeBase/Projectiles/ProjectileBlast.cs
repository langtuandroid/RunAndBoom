using System.Collections;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles
{
    [RequireComponent(typeof(DestroyWithBlast), typeof(ProjectileTrace))]
    public class ProjectileBlast : MonoBehaviour
    {
        private DestroyWithBlast _destroyWithBlast;
        private ProjectileTrace _projectileTrace;
        private GameObject _blastVfxPrefab;
        private float _sphereRadius;
        private ProjectileMovement _movement;
        private ParticleSystem _particleSystem;
        private GameObject _blastVfx;

        private void Awake()
        {
            _destroyWithBlast = GetComponent<DestroyWithBlast>();
            _projectileTrace = GetComponent<ProjectileTrace>();
            _movement = GetComponent<ProjectileMovement>();
        }

        // private void OnEnable() => 
        //     HideBlast();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Environments") || collision.gameObject.CompareTag("Floor"))
            {
                ShowBlast();

                StartCoroutine(DestroyBlast());

                _destroyWithBlast.DestroyAllAround(_sphereRadius);
                _projectileTrace.DestroyTrace();
                _movement.Stop();
                gameObject.SetActive(false);
            }

            // if (collision.gameObject.CompareTag("Hero"))
            // {
            //     HeroHealth heroHealth = collision.gameObject.GetComponent<HeroHealth>();
            //     heroHealth.TakeDamage(1);
            // }
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
            yield return new WaitForSeconds(2f);
            HideBlast();
        }
    }
}