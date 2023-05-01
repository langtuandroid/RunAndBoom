using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private const float ZeroSpeed = 0f;

        private Transform _heroTransform;
        private float _attackDistance;
        private float _speed;

        public bool IsMove => _agent.enabled;

        // private void OnEnable() =>
        //     _agent.enabled = true;
        //
        // private void OnDisable() =>
        //     _agent.enabled = false;

        private void Update() =>
            SetDestinationForAgent();

        // public void Construct(Transform heroTransform)
        public void Construct(Transform heroTransform, float attackDistance, float speed)
        {
            _speed = speed;
            _attackDistance = attackDistance;
            _heroTransform = heroTransform;
            _agent.destination = transform.position;
            // IsMove = false;
            Stop();
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

                        Vector3 heroTransformPosition = _heroTransform.position - (direction * _attackDistance);
                        _agent.destination = heroTransformPosition;

                        Debug.Log($"distance: {distance}");
                        Debug.Log($"attackDistance: {_attackDistance}");

                        if (distance > _attackDistance)
                        {
                            _agent.destination = heroTransformPosition;
                            // _agent.enabled = true;
                            // Move();
                        }
                        else
                        {
                            // IsMove = false;
                            // _agent.enabled = false;
                            Stop();
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
            // IsMove = true;
            _agent.enabled = true;
            _agent.isStopped = false;
            _agent.speed = _speed;
        }

        public override void Stop()
        {
            // IsMove = false;
            _agent.enabled = false;
            // _agent.isStopped = true;
            // _agent.speed = ZeroSpeed;
        }
    }
}