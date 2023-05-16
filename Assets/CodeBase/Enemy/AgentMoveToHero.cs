using System;
using CodeBase.Data;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        private bool _move;
        private float _attackDistance;
        private float _speed;

        private void OnEnable() =>
            _agent.enabled = true;

        private void OnDisable() =>
            _agent.enabled = false;

        private void Update() =>
            SetDestinationForAgent();

        public void Construct(Transform heroTransform, float attackDistance, float speed)
        {
            _heroTransform = heroTransform;
            _speed = speed;
            _attackDistance = attackDistance;
            _agent.enabled = false;
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform && _agent != null)
            {
                if (_move && _agent.enabled && IsHeroNotReached())
                {
                    try
                    {
                        _agent.destination = _heroTransform.position;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Debug.Log($"SetDestinationForAgent error: {e}");
                        throw;
                    }
                }
            }
        }

        public override void Move()
        {
            _move = true;
            _agent.enabled = true;
            _agent.speed = _speed;
        }

        public override void Stop()
        {
            _move = false;
            _agent.speed = 0f;
            _agent.enabled = false;
        }

        private bool IsHeroNotReached() =>
            _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= _attackDistance;
    }
}