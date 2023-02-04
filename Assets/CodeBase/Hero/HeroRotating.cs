using System;
using System.Collections;
using CodeBase.Enemy;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroRotating : MonoBehaviour
    {
        private float _rotationSpeed = 0.05f;

        private HeroAiming _heroAiming;
        private Vector3 _shootPosition;
        private Quaternion _startGameRotation;
        private Vector3 _direction;
        private bool _rotating = false;
        private float _angle;

        private Coroutine _rotatingToClickCoroutine;
        private Coroutine _rotatingToClosesEnemyCoroutine;
        private Coroutine _lookAtCoroutine;

        public event Action<Vector3> ShootDirection;

        private void Awake()
        {
        }

        [Inject]
        public void Construct()
        {
            _heroAiming = GetComponent<HeroAiming>();
            _heroAiming.FoundClosestEnemy += RotateTo;
        }
        private void RotateTo(EnemyHealth enemy)
        {
            if (_rotatingToClickCoroutine != null) StopCoroutine(_rotatingToClickCoroutine);

            _rotatingToClickCoroutine = StartCoroutine(CoroutineRotateTo(enemy));
        }

        private IEnumerator CoroutineRotateTo(EnemyHealth enemy)
        {
            _rotating = true;
            _shootPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);

            while (_rotating)
            {
                _direction = (_shootPosition - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    _rotationSpeed);

                _angle = Vector3.Angle(transform.forward, _direction);

                if (_angle < 5f)
                {
                    transform.LookAt(_shootPosition, Vector3.up);
                    ShootDirection?.Invoke(enemy.gameObject.transform.position);
                    _rotating = false;
                }

                yield return null;
            }
        }
    }
}