using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _sensitivity = 1.0f;

        private const float EdgeAngle = 87f;
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

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
            float mouseX = Input.GetAxis(MouseX);
            float mouseY = Input.GetAxis(MouseY);

            float rotationAmountX = mouseX * _sensitivity;
            float rotationAmountY = mouseY * _sensitivity;

            _xAxisClamp -= rotationAmountY;

            Vector3 rotation = _camera.transform.rotation.eulerAngles;

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

            _camera.transform.rotation = Quaternion.Euler(rotation);
        }

        public void TurnOn() =>
            _canRotate = true;

        public void TurnOff() =>
            _canRotate = false;
    }
}