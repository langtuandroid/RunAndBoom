using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        private bool _move;

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
                Debug.Log($"speed {_agent.speed}");

                if (_move)
                    _agent.destination = _heroTransform.position;
                else
                    _agent.speed = 0f;
            }
        }

        public override void Move()
        {
            _agent.enabled = true;
            _move = true;
        }

        public override void Stop()
        {
            _agent.enabled = false;
            _move = false;
        }
    }
}