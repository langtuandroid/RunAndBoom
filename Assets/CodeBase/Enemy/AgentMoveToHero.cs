using System;
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
            _agent.destination = transform.position;
            _agent.enabled = false;
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform && _agent != null)
            {
                if (_move && _agent.enabled)
                {
                    try
                    {
                        // Vector3 heading = _heroTransform.position - transform.position;
                        // float distance = heading.magnitude;
                        // Vector3 direction = heading / distance;
                        //
                        // Vector3 heroTransformPosition = _heroTransform.position - (direction * 1.5f);
                        // _agent.destination = heroTransformPosition;
                        _agent.destination = _heroTransform.position;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Debug.Log($"SetDestinationForAgent error: {e}");
                        throw;
                    }
                }
                else
                {
                    _agent.destination = transform.position;
                }
            }
        }

        public override void Move()
        {
            _move = true;
            // _agent.enabled = true;
        }

        public override void Stop()
        {
            _move = false;
            // _agent.enabled = false;
        }
    }
}