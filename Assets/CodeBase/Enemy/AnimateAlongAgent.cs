using System;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour, IOnOffable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private const float MinimalVelocity = 0.1f;

        private bool _run;

        private void Awake() => 
            On();

        private void Update()
        {
            if (ShouldMove() && _run)
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

        public void On() =>
            _run = true;

        public void Off() =>
            _run = false;
    }
}