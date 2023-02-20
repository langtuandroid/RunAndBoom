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

        private const float RotationSpeed = 0.01f;
        private const float SmoothRotationSpeed = 0.02f;
        private const float AngleForFastRotating = 10f;
        private const float MaxAngleForLookAt = 2f;
        private float _anglesToRotate = 5f;

        private Vector3 _forwardDirection;
        private Rigidbody _rigidbody;
        private Vector3 _shootPosition;
        private Vector3 _direction;
        private bool _toEnemy;

        private Coroutine _rotatingToEnemyCoroutine;
        private Coroutine _rotatingToForwardCoroutine;
        private bool _lookForward = false;
        private Quaternion _targetRotation;

        public event Action<GameObject> EndedRotatingToEnemy;
        public event Action StartedRotating;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _enemiesChecker.FoundClosestEnemy += RotateTo;
            _enemiesChecker.EnemyNotFound += RotateToForward;
            _heroLookAt.LookedAtEnemy += StopRotatingToEnemy;
            _forwardDirection = transform.eulerAngles;
        }

        private void RotateTo(GameObject enemy)
        {
            Debug.Log("RotateToEnemy");
            if (_rotatingToEnemyCoroutine != null)
                StopCoroutine(_rotatingToEnemyCoroutine);
            if (_rotatingToForwardCoroutine != null)
                StopCoroutine(_rotatingToForwardCoroutine);

            var position = enemy.transform.position;
            _shootPosition = new Vector3(position.x, position.y + Constants.AdditionYToEnemy, position.z);
            _rotatingToEnemyCoroutine = StartCoroutine(CoroutineRotateTo(enemy));
        }

        private void RotateToForward()
        {
            Debug.Log("RotateToForward");
            if (_rotatingToEnemyCoroutine != null)
                StopCoroutine(_rotatingToEnemyCoroutine);

            _rotatingToForwardCoroutine = StartCoroutine(CoroutineRotateToForward());
        }

        private IEnumerator CoroutineRotateToForward()
        {
            Debug.Log("CoroutineRotateToForward");
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
                Debug.Log($"angle {angle}");

                if (angle < AngleForFastRotating)
                    transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, SmoothRotationSpeed);
                else
                    transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, RotationSpeed);

                yield return null;
            }
        }

        private IEnumerator CoroutineRotateTo(GameObject enemy)
        {
            Debug.Log("CoroutineRotateTo");
            StartedRotating?.Invoke();
            _direction = (_shootPosition - transform.position).normalized;
            _targetRotation = Quaternion.LookRotation(_direction);
            float angle = Vector3.Angle(transform.forward, _direction);

            while (angle > 0f)
            {
                angle = Vector3.Angle(transform.forward, _direction);
                Debug.Log($"angle {angle}");

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
            Debug.Log("StopRotating");
            if (_rotatingToEnemyCoroutine != null)
                StopCoroutine(_rotatingToEnemyCoroutine);
        }
    }
}