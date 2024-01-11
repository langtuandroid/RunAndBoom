using CodeBase.Logic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class StopMovingOnAttack : MonoBehaviour, IOnOffable
    {
        private EnemyAnimator _animator;
        private NavMeshAgent _agent;
        private bool _run;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
        }

        private void Start()
        {
            On();
            _animator.StateEntered += SwitchMovementOff;
            _animator.StateExited += SwitchMovementOn;
        }

        private void SwitchMovementOn(AnimatorState animatorState)
        {
            if (animatorState == AnimatorState.Attack && _run)
                _agent.isStopped = false;
        }

        private void SwitchMovementOff(AnimatorState animatorState)
        {
            if (animatorState == AnimatorState.Attack && _run)
                _agent.isStopped = true;
        }

        public void On() =>
            _run = true;

        public void Off() =>
            _run = false;
    }
}