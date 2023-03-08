using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy.Attacks
{
    public class WithBatAttack : Attack
    {
        private const float YLevitation = 0.5f;
        private const int DrawingHitTime = 1;

        private float _cleavage;
        private float _effectiveDistance;
        private int _damage;

        private int _layerMask;
        private readonly Collider[] _hits = new Collider[1];

        private void Awake() =>
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HeroTag);

        public void Construct(Transform heroTransform, float attackCooldown, float cleavage, float effectiveDistance, int damage)
        {
            base.Construct(heroTransform, attackCooldown);
            _cleavage = cleavage;
            _effectiveDistance = effectiveDistance;
            _damage = damage;
        }

        protected override void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _cleavage, DrawingHitTime);
                hit.transform.parent.gameObject.GetComponent<IHealth>().TakeDamage(_damage);
                Debug.Log($"{gameObject.name} hit Hero with {_damage} damage");
            }
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            Vector3 position = transform.position;
            return new Vector3(position.x, position.y + YLevitation, position.z) +
                   transform.forward * _effectiveDistance;
        }
    }
}