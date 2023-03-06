using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Level
{
    public class DoorMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _door;
        [SerializeField] private LevelSectorTrigger _trigger;

        private float _speed;
        private float _minY;
        private float _maxY;
        private float _positionY;
        private float _targetY;
        private Transform _doorTransform;
        private bool _close;

        private Coroutine _movementCoroutine;

        private void Awake()
        {
            _doorTransform = _door.GetComponent<Transform>();
            _positionY = _door.transform.position.y;
            _targetY = _positionY;
            _minY = _positionY - _door.GetComponent<MeshRenderer>().bounds.size.y;
            _maxY = _positionY;

            _trigger.Passed += Close;
        }

        private void Close()
        {
            _targetY = _maxY;
            _close = true;
        }

        private void Update() =>
            _doorTransform.position = Vector3.MoveTowards(_doorTransform.position,
                new Vector3(_doorTransform.position.x, _targetY, _doorTransform.position.z), _speed * Time.deltaTime);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareByTag("Hero"))
                if (_close == false)
                {
                    _targetY = _minY;
                    Debug.Log($"minY {_minY}");
                }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareByTag("Hero"))
                if (_close == false)
                {
                    _targetY = _maxY;
                    Debug.Log($"maxY {_maxY}");
                }
        }
    }
}