using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        private bool _run;

        private void OnEnable() =>
            _agent.enabled = true;

        private void OnDisable() =>
            _agent.enabled = false;

        private void Update()
        {
            if (_run)
                SetDestinationForAgent();
            
            Debug.Log($"agent.enabled: {_agent.enabled}");
        }

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
            _agent.enabled = false;
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform) 
                _agent.destination = _heroTransform.position;
        }

        public override void Run()
        {
            _agent.enabled = true;
            _run = true;
        }

        public override void Stop()
        {
            _agent.enabled = false;
            _run = false;
        }
    }
}