using System;
using System.Collections;
using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.Services.Input.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 5f;

        private IPlatformInputService _platformInputService;

        private EnemiesChecker _enemiesChecker;
        private HeroShooting _heroShooting;
        private GameObject _rotatingBody;
        private Vector3 _shootPosition;
        private Quaternion _startGameRotation;
        private Vector3 _direction;
        private bool _rotating = false;
        private bool _shootPositionExists = false;
        private float _angle;

        private Coroutine _rotatingToClickCoroutine;
        private Coroutine _rotatingToClosesEnemyCoroutine;
        private Coroutine _lookAtCoroutine;

        public event Action<IHealth> ShootEnemy;
        public event Action<Vector3> ShootDirection;

        private void Awake()
        {
            _enemiesChecker = GetComponent<EnemiesChecker>();
            _rotatingBody = gameObject;

            _platformInputService.Shot += RotateTo;
            _enemiesChecker.FoundClosestEnemy += RotateToPoint;
        }

        [Inject]
        public void Construct(IPlatformInputService platformInputService) =>
            _platformInputService = platformInputService;

        // private void OnDisable()
        // {
        //     _platformInputService.Shot -= RotateToPoint;
        //     _enemiesChecker.FoundClosestEnemy -= RotateToPoint;
        // }

        private void Update()
        {
            if (!_rotating && _shootPositionExists)
            {
                _rotatingBody.transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
                // _rotatingBody.transform.LookAt(_shootPosition);
                //     _rotatingBody.transform.DORotate(_shootPosition, 0f);

                _angle = Vector3.Angle(_rotatingBody.transform.forward, _direction.normalized);
            }
        }

        private void RotateTo(Vector2 target)
        {
            if (_rotatingToClickCoroutine != null)
                StopCoroutine(_rotatingToClickCoroutine);

            if (_lookAtCoroutine != null) StopCoroutine(_lookAtCoroutine);

            _rotatingToClickCoroutine = StartCoroutine(DoRotateTo(target));
        }

        private IEnumerator DoRotateTo(Vector2 target)
        {
            _rotating = true;
            _shootPositionExists = true;
            Vector3 position = _rotatingBody.transform.position;
            _shootPosition = new Vector3(target.x, position.y, target.y
            );
            // _shootPosition = new Vector3(target.x, _rotatingBody.transform.position.y, target.z);
            // Debug.Log($"_shootPosition {_shootPosition}");
            // Debug.Log($"transform.position {transform.position}");

            while (_rotating)
            {
                // _direction = (_shootPosition - _rotatingBody.transform.position).normalized;
                // // Debug.Log($"direction {_direction}");
                // Quaternion targetRotation = Quaternion.LookRotation(_direction);
                // _rotatingBody.transform.rotation = Quaternion.Slerp(_rotatingBody.transform.rotation, targetRotation,
                //     _rotationSpeed);
                // _angle = Vector3.Angle(_rotatingBody.transform.forward, _direction);
                //
                // if (_rotatingBody.transform.forward == _direction)
                // {
                //     ShootDirection?.Invoke();
                //     _rotating = false;
                //     _shootPositionExists = false;
                //
                //     // _lookAt = true;
                //     // _lookAtCoroutine = StartCoroutine(DoLookAt(fixedTarget));
                //     // _rotatingBody.transform.LookAt(_shootPosition, Vector3.up);
                // }

                ///
                /// 
                _direction = (_shootPosition - _rotatingBody.transform.position).normalized;
                Debug.Log($"DoRotateToPoint direction {_direction}");
                Quaternion targetRotation = Quaternion.LookRotation(_direction);
                Debug.Log($"DoRotateToPoint targetRotation {targetRotation}");
                _rotatingBody.transform.rotation = Quaternion.Slerp(_rotatingBody.transform.rotation, targetRotation,
                    _rotationSpeed);
                Debug.Log($"DoRotateToPoint current rotation {_rotatingBody.transform.rotation}");

                _angle = Vector3.Angle(_rotatingBody.transform.forward, _direction);

                if (_angle < 5f)
                {
                    // _rotatingBody.transform.LookAt(_shootPosition, Vector3.up);
                    ShootDirection?.Invoke(_direction);
                    _rotating = false;
                }

                yield return null;
            }
        }

        private void RotateToPoint(EnemyHealth enemy)
        {
            if (_rotatingToClickCoroutine != null) StopCoroutine(_rotatingToClickCoroutine);

            _rotatingToClickCoroutine = StartCoroutine(DoRotateTo(enemy));
        }

        private IEnumerator DoRotateTo(EnemyHealth enemy)
        {
            _rotating = true;
            _shootPosition = new Vector3(enemy.transform.position.x, _rotatingBody.transform.position.y,
                enemy.transform.position.z);
            _shootPositionExists = true;

            while (_rotating)
            {
                _direction = (_shootPosition - _rotatingBody.transform.position).normalized;
                Debug.Log($"DoRotateToPoint direction {_direction}");
                Quaternion targetRotation = Quaternion.LookRotation(_direction);
                Debug.Log($"DoRotateToPoint targetRotation {targetRotation}");
                _rotatingBody.transform.rotation = Quaternion.Slerp(_rotatingBody.transform.rotation, targetRotation,
                    _rotationSpeed);
                Debug.Log($"DoRotateToPoint current rotation {_rotatingBody.transform.rotation}");

                _angle = Vector3.Angle(_rotatingBody.transform.forward, _direction);

                if (_angle < 5f)
                {
                    _rotatingBody.transform.LookAt(_shootPosition, Vector3.up);
                    // int points = 0;
                    // enemy.TakeDamage(ref points);
                    // _gameWindow.AddScore(points);
                    // _gameWindow.MinusScore(_weapon.ShotPoints);
                    // Debug.Log("DoRotateToPoint Shoot");
                    GameObject enemyObject = enemy.gameObject;
                    IHealth enemyHealth = enemyObject.GetComponent<IHealth>();
                    ShootEnemy?.Invoke(enemyHealth);
                    _rotating = false;
                }

                // Debug.Log($"DoRotateToPoint _angle {_angle}");

                // if (_rotatingBody.transform.forward == _direction.normalized)
                // {
                // _rotatingBody.transform.LookAt(_shootPosition, Vector3.up);
                // }

                // Debug.Log($"DoRotateToPoint _rotating {_rotating}");

                yield return null;
            }
        }

        // private void LookAt(Vector3 target)
        // {
        //     while (!_rotating)
        //     {
        //         _rotatingBody.transform.LookAt(target, Vector3.up);
        //         // Debug.Log($"_rotatingBody.transform.rotation {_rotatingBody.transform.rotation}");
        //     }
        // }
    }
}