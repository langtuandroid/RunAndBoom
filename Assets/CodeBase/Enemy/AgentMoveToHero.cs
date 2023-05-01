using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        public bool IsMove { get; private set; }

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
                if (IsMove
                    // && _agent.enabled
                   )
                {
                    try
                    {
                        Vector3 heading = _heroTransform.position - transform.position;
                        float distance = heading.magnitude;
                        Vector3 direction = heading / distance;

                        Vector3 heroTransformPosition = _heroTransform.position - (direction * 1.5f);

                        if (distance > 1.5f)
                        {
                            _agent.enabled = true;
                            _agent.destination = heroTransformPosition;
                        }
                        else
                        {
                            IsMove = false;
                            _agent.enabled = false;
                        }
                        // _agent.destination = _heroTransform.position;
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
                    // _agent.destination = transform.position;
                }
            }
        }

        public override void Move()
        {
            IsMove = true;
            _agent.enabled = true;
        }

        public override void Stop()
        {
            IsMove = false;
            _agent.enabled = false;
        }
    }
}