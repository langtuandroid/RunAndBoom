using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroRunner : MonoBehaviour
    {
        [SerializeField] private float _maxRunSpeed = 5f;
        [SerializeField] private float _minRunSpeed = 2f;

        private const string StopTag = "Stop";

        private float _startX;
        private float _currentRunSpeed;
        private Rigidbody _rigidbody;

        private GameObject _rotatingBody;
        // private float _passedDistance;

        // public event Action<float, float> ChangedDistance;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rotatingBody = transform.GetChild(0).gameObject;
        }

        private void Start()
        {
            _currentRunSpeed = _maxRunSpeed;
            _startX = _rotatingBody.transform.position.x;
        }

        private void Update()
        {
            RunForward();
            // if (transform.position.z % 0.2f == 0)
            // {
            // _passedDistance += 0.2f;
            // float remainingDistanceMetres = Mathf.Round(_roadGenerator.PlayerPath - _passedDistance);
            // float remainingDistancePercents =
            //     Mathf.Round((_roadGenerator.PlayerPath - _passedDistance) / _roadGenerator.PlayerPath);

            // ChangedDistance?.Invoke(remainingDistanceMetres, remainingDistancePercents);
            // }
        }

        private void RunForward()
        {
            _rigidbody.velocity = transform.forward * _currentRunSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.CompareByTag(StopTag))
                _currentRunSpeed = 0f;
        }
    }
}