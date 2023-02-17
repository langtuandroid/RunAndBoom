using System;
using System.Collections;
using CodeBase.Enemy;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private EnemiesChecker _enemiesChecker;

        private const float RotationSpeed = 0.01f;
        private const float SmoothRotationSpeed = 0.02f;
        private const float AngleForSmoothRotating = 10f;
        private const float MaxAngleForLookAt = 5f;

        private Vector3 _shootPosition;
        private Vector3 _direction;
        private bool _rotating = false;
        private float _angle;
        private string _currentEnemyId;
        private GameObject _currentEnemy = null;

        private Coroutine _rotatingToEnemyCoroutine;

        public event Action<GameObject> EndedRotatingToEnemy;
        public event Action EndedRotatingToForward;
        public event Action StartedRotating;

        [Inject]
        public void Construct()
        {
            _enemiesChecker.FoundClosestEnemy += RotateTo;
            _enemiesChecker.EnemyNotFound += RotateToForward;
        }

        private void RotateToForward()
        {
            Debug.Log("RotateToForward");
            if (_rotatingToEnemyCoroutine != null)
                StopCoroutine(_rotatingToEnemyCoroutine);

            _rotatingToEnemyCoroutine = StartCoroutine(CoroutineRotateTo(Vector3.forward));
        }

        private void RotateTo(GameObject enemy)
            // private void RotateTo(EnemyHealth enemy)
        {
            if (_currentEnemy == null || _currentEnemy.GetComponent<UniqueId>().Id != enemy.GetComponent<UniqueId>().Id)
            {
                if (_rotatingToEnemyCoroutine != null)
                    StopCoroutine(_rotatingToEnemyCoroutine);

                Debug.Log("StartCoroutine");
                _rotatingToEnemyCoroutine = StartCoroutine(CoroutineRotateTo(enemy));
            }
        }

        private IEnumerator CoroutineRotateTo(GameObject enemy)
            // private IEnumerator CoroutineRotateTo(EnemyHealth enemy)
        {
            Debug.Log("CoroutineRotateTo");
            _currentEnemy = enemy;
            _rotating = true;
            // _shootPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);

            while (IsRotatingToAliveEnemy(_currentEnemy.GetComponent<EnemyHealth>().Current))
                // while (IsRotatingToAliveEnemy(enemy.Current))
            {
                Debug.Log("while true");
                _shootPosition = new Vector3(_currentEnemy.transform.position.x, transform.position.y, _currentEnemy.transform.position.z);

                if (IsEnemyLookedAt(enemy.GetComponent<EnemyHealth>()))
                    // if (IsEnemyLookedAt(enemy))
                {
                    Debug.Log("IsEnemyLookedAt true");
                    LookAt();
                }
                else
                {
                    Debug.Log("IsEnemyLookedAt false");
                    Rotate();
                    _currentEnemyId = enemy.gameObject.GetComponent<UniqueId>().Id;
                }

                yield return null;
            }
        }

        private bool IsRotatingToAliveEnemy(int health) =>
            _rotating && health > 0;

        private bool IsEnemyLookedAt(EnemyHealth enemy) =>
            _currentEnemyId == enemy.gameObject.GetComponent<UniqueId>().Id && _rotating == false;

        private IEnumerator CoroutineRotateTo(Vector3 target)
        {
            Debug.Log("CoroutineRotateTo");
            _currentEnemy = null;

            _rotating = true;
            _shootPosition = new Vector3(target.x, transform.position.y, target.z);

            while (_rotating)
            {
                Rotate();

                yield return null;
            }
        }

        private void Rotate()
        {
            Debug.Log("Rotate");
            StartedRotating?.Invoke();
            _direction = (_shootPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(_direction);

            _angle = Vector3.Angle(transform.forward, _direction);
            Debug.Log($"angle {_angle}");

            if (_angle < AngleForSmoothRotating)
            {
                if (_angle < MaxAngleForLookAt)
                    LookAt();
                else
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, SmoothRotationSpeed);
            }
            else
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed);
        }

        private void LookAt()
        {
            _rotating = false;
            
            if (_currentEnemy != null)
                EndedRotatingToEnemy?.Invoke(_currentEnemy);
            else
                EndedRotatingToForward?.Invoke();
        }
    }
}