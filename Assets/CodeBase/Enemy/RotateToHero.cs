using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : MonoBehaviour, IOnOffable
    {
        [SerializeField] private float _speed;

        private Transform _heroTransform;
        private Vector3 _directionToLook;
        private Quaternion _targetRotation;
        private bool _run;

        private void Update()
        {
            if (_heroTransform && _run)
                RotateTowardsHero();
        }

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
            On();
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();
            _targetRotation = TargetRotation(_directionToLook);
            transform.rotation = SmoothedRotation(transform.rotation, _targetRotation);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDelta = _heroTransform.position - transform.position;
            _directionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z).normalized;
        }

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position, Vector3.up);

        private Quaternion SmoothedRotation(Quaternion rotation, Quaternion targetRotation) =>
            Quaternion.Lerp(rotation, targetRotation, SpeedFactor());

        private float SpeedFactor() =>
            _speed * Time.deltaTime;

        public void On() =>
            _run = true;

        public void Off() =>
            _run = false;
    }
}