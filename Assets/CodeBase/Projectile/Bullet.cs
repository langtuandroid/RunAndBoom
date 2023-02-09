using System.Collections;
using CodeBase.StaticData.ProjectileTrace;
using UnityEngine;

namespace CodeBase.Projectile
{
    public class Bullet : Projectile
    {
        private float _speed;
        private Vector3 _target;

        public void Construct(GameObject blastVfx, ProjectileTraceStaticData projectileTraceStaticData, float speed, float blastRadius, Vector3 target)
        {
            _speed = speed * 5f;
            _target = target;
            base.Construct(blastVfx, projectileTraceStaticData, blastRadius);
        }

        public override void Launch()
        {
            StartCoroutine(CoroutineLaunch());
        }

        private IEnumerator CoroutineLaunch()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            Debug.Log($"bullet pos: {transform.position}");
            yield return null;
        }
    }
}