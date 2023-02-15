using UnityEngine;

namespace CodeBase.Level
{
    public class DoorMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _door;
        [SerializeField] private float _speed;

        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        private Coroutine _movementCoroutine;
        private float _positionY;
        private float _targetY;
        private Transform _doorTransform;

        private void Awake()
        {
            _doorTransform = _door.GetComponent<Transform>();
            _positionY = _door.transform.position.y;
            _targetY = _positionY;
            _minY = _positionY - _door.GetComponent<MeshRenderer>().bounds.size.y;
            _maxY = _positionY;
        }

        private void Update() =>
            _doorTransform.position = Vector3.MoveTowards(_doorTransform.position,
                new Vector3(_doorTransform.position.x, _targetY, _doorTransform.position.z), _speed * Time.deltaTime);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hero"))
            {
                _targetY = _minY;
                Debug.Log("OnTriggerEnter");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Hero"))
            {
                _targetY = _maxY;
                Debug.Log("OnTriggerExit");
            }
        }
    }
}