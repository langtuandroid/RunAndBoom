using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        private void Update() =>
            SetDestinationForAgent();

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
                _agent.destination = _heroTransform.position;
        }
    }
}