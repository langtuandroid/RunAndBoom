using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        private void OnEnable() =>
            _agent.enabled = true;

        private void OnDisable() =>
            _agent.enabled = false;

        private void Update() =>
            SetDestinationForAgent();

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
            _agent.enabled = false;
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
            {
                _agent.destination = _heroTransform.position;
                // _agent.SetDestination(_heroTransform.position);
                // _agent.speed = 3f;
                // Debug.Log($"hero position {_heroTransform.position}");
            }
        }
    }
}