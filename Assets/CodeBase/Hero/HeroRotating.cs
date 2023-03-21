using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private EnemiesChecker _enemiesChecker;
        [SerializeField] private HeroLookAt _heroLookAt;

        private const float RotationSpeed = 0.02f;
        private const float SmoothRotationSpeed = 0.04f;
        private const float AngleForFastRotating = 10f;
        private const float MaxAngleForLookAt = 1f;

        private Vector3 _shootPosition;
        private Vector3 _direction;
        private bool _toEnemy;
        private Quaternion _targetRotation;
        private Coroutine _rotatingCoroutine;
        private bool _canRotating = true;

        public event Action<GameObject> EndedRotatingToEnemy;
        public event Action StartedRotating;

        private void Awake()
        {
            _enemiesChecker.FoundClosestEnemy += RotateTo;
            _enemiesChecker.EnemyNotFound += RotateToForward;
            _heroLookAt.LookedAtEnemy += StopRotatingToEnemy;
        }

        public void TurnOff() =>
            StopRotatingToEnemy();

        private void RotateTo(GameObject enemy)
        {
            if (_rotatingCoroutine != null)
                StopCoroutine(_rotatingCoroutine);

            var position = enemy.transform.position;
            _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z);

            _rotatingCoroutine = StartCoroutine(CoroutineRotateTo(enemy));
        }

        private void RotateToForward()
        {
            if (_rotatingCoroutine != null)
                StopCoroutine(_rotatingCoroutine);

            _rotatingCoroutine = StartCoroutine(CoroutineRotateToForward());
        }

        private IEnumerator CoroutineRotateToForward()
        {
            StartedRotating?.Invoke();
            var position = transform.position;
            _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z + 50f);
            _direction = (_shootPosition - position).normalized;
            _targetRotation = Quaternion.LookRotation(_direction);
            float angle = Vector3.Angle(transform.forward, _direction);

            while (angle > 0f)
            {
                _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z + 50f);
                _direction = (_shootPosition - position).normalized;
                _targetRotation = Quaternion.LookRotation(_direction);
                angle = Vector3.Angle(transform.forward, _direction);

                if (angle < AngleForFastRotating)
                    transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, SmoothRotationSpeed);
                else
                    transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, RotationSpeed);

                yield return null;
            }
        }

        private IEnumerator CoroutineRotateTo(GameObject enemy)
        {
            StartedRotating?.Invoke();
            _direction = (_shootPosition - transform.position).normalized;
            _targetRotation = Quaternion.LookRotation(_direction);
            float angle = Vector3.Angle(transform.forward, _direction);

            while (angle > 0f)
            {
                angle = Vector3.Angle(transform.forward, _direction);

                if (angle < AngleForFastRotating)
                {
                    if (angle < MaxAngleForLookAt)
                        EndedRotatingToEnemy?.Invoke(enemy);
                    else
                        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, SmoothRotationSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, RotationSpeed);
                }

                yield return null;
            }
        }

        private void StopRotatingToEnemy()
        {
            if (_rotatingCoroutine != null)
                StopCoroutine(_rotatingCoroutine);
        }
    }
}