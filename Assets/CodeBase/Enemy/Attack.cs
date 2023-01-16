using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        private const float YLevitation = 0.5f;
        [SerializeField] private EnemyAnimator _animator;

        public float AttackCooldown = 1f;
        public float Cleavage = YLevitation;
        public float EffectiveDistance = YLevitation;
        public int Damage = 10;

        private Transform _heroTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void UpdateCooldown()
        {
            if (!CooldownUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() =>
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + YLevitation, transform.position.z) +
            transform.forward * EffectiveDistance;

        private void OnAttackEnded()
        {
            _currentAttackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        private bool CooldownUp() =>
            _currentAttackCooldown <= 0;

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownUp();
    }
}