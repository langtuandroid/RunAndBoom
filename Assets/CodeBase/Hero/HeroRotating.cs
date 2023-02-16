using System.Collections;
using CodeBase.Enemy;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroRotating : MonoBehaviour
    {
        private const float RotationSpeed = 0.01f;

        private EnemiesChecker _enemiesChecker;
        private Vector3 _shootPosition;
        private Quaternion _startGameRotation;
        private Vector3 _direction;
        private bool _rotating = false;
        private float _angle;

        private Coroutine _rotatingToEnemyCoroutine;

        [Inject]
        public void Construct()
        {
            _enemiesChecker = GetComponent<EnemiesChecker>();
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

        private void RotateTo(EnemyHealth enemy)
        {
            if (_rotatingToEnemyCoroutine != null)
                StopCoroutine(_rotatingToEnemyCoroutine);

            _rotatingToEnemyCoroutine = StartCoroutine(CoroutineRotateTo(enemy));
        }

        private IEnumerator CoroutineRotateTo(EnemyHealth enemy)
        {
            Debug.Log("CoroutineRotateTo");
            _rotating = true;
            _shootPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);

            while (_rotating && enemy.Current > 0)
            {
                Rotate();

                yield return null;
            }
        }

        private IEnumerator CoroutineRotateTo(Vector3 target)
        {
            Debug.Log("CoroutineRotateTo");
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
            _direction = (_shootPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed);

            _angle = Vector3.Angle(transform.forward, _direction);

            if (_angle < 0.5f)
                transform.LookAt(_shootPosition, Vector3.up);
        }
    }
}