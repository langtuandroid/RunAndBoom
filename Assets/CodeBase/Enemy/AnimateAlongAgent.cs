using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private const float MinimalVelocity = 0.1f;

        private void Update()
        {
            if (ShouldMove())
                _animator.Move();
            else
                _animator.StopMoving();
        }

        private bool ShouldMove()
        {
            if (_agent != null)
                return _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
            else
                return false;
        }
    }
}