using UnityEngine;

namespace CodeBase.Enemy.Attacks
{
    public class WithPistolAttack : Attack
    {
        private const float YLevitation = 0.5f;

        private float _cleavage;
        private float _effectiveDistance;
        private int _damage;

        private int _layerMask;
        private Collider[] _hits = new Collider[1];

        private void Awake() =>
            _layerMask = 1 << LayerMask.NameToLayer("Hero");

        public void Construct(Transform heroTransform, float attackCooldown, float cleavage, float effectiveDistance, int damage)
        {
            base.Construct(heroTransform, attackCooldown);
            _cleavage = cleavage;
            _effectiveDistance = effectiveDistance;
            _damage = damage;
        }

        protected void OnAttack()
        {
            Debug.Log($"{gameObject.name} hit Hero");
        }
    }
}