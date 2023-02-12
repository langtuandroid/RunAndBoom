﻿using System.Collections;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Projectiles
{
    [RequireComponent(typeof(DestroyWithBlast), typeof(ProjectileTrace))]
    public class ProjectileBlast : MonoBehaviour
    {
        private DestroyWithBlast _destroyWithBlast;
        private ProjectileTrace _projectileTrace;
        private GameObject _blastVfx;
        private float _sphereRadius;
        private ProjectileMovement _movement;

        private void Awake()
        {
            _destroyWithBlast = GetComponent<DestroyWithBlast>();
            _projectileTrace = GetComponent<ProjectileTrace>();
            _movement = GetComponent<ProjectileMovement>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Environments") || collision.gameObject.CompareTag("Floor"))
            {
                Debug.Log("Blast!");
                var blastVfx = Instantiate(_blastVfx, transform.position, Quaternion.identity);
                StartCoroutine(DestroyBlast(blastVfx));

                _destroyWithBlast.DestroyAllAround(_sphereRadius);
                _projectileTrace.DestroyTrace();
                _movement.Stop();
                gameObject.SetActive(false);
            }

            if (collision.gameObject.CompareTag("Hero"))
            {
                HeroHealth heroHealth = collision.gameObject.GetComponent<HeroHealth>();
                heroHealth.TakeDamage(1);
            }
        }

        public void Construct(GameObject blastVfxPrefab, float blastRadius)
        {
            _blastVfx = blastVfxPrefab;
            _sphereRadius = blastRadius;
        }

        private IEnumerator DestroyBlast(GameObject blastVfx)
        {
            yield return new WaitForSeconds(2f);
            blastVfx.SetActive(false);
        }
    }
}