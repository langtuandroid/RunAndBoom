using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 1.0f;

        private const float EdgeAngle = 87f;

        private float _xAxisClamp = 0;
        private bool _canRotate = true;

        private void Start() =>
            Cursor.lockState = CursorLockMode.Locked;

        private void Update()
        {
            if (_canRotate)
                Rotate();
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float rotationAmountX = mouseX * _sensitivity;
            float rotationAmountY = mouseY * _sensitivity;

            _xAxisClamp -= rotationAmountY;

            Vector3 rotation = transform.rotation.eulerAngles;

            rotation.x -= rotationAmountY;
            rotation.y += rotationAmountX;

            switch (_xAxisClamp)
            {
                case > EdgeAngle:
                    _xAxisClamp = EdgeAngle;
                    rotation.x = EdgeAngle;
                    break;
                case < -EdgeAngle:
                    _xAxisClamp = -EdgeAngle;
                    rotation.x = -EdgeAngle;
                    break;
            }

            transform.rotation = Quaternion.Euler(rotation);
        }

        public void TurnOn() =>
            _canRotate = true;

        public void TurnOff() =>
            _canRotate = false;
    }
}