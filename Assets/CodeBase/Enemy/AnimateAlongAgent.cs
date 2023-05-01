using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private AgentMoveToHero _agentMoveToHero;

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
            {
                bool shouldMove = _agent.velocity.magnitude > MinimalVelocity &&
                                  _agent.remainingDistance > _agent.radius &&
                                  _agentMoveToHero.IsMove;
                // Debug.Log($"_agent.velocity.magnitude: {_agent.velocity.magnitude}");
                Debug.Log($"shouldMove: {shouldMove}");
                return shouldMove;
            }
            else
            {
                return false;
            }
        }
    }
}