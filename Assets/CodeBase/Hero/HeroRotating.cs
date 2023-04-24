using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 1.0f;
        [SerializeField] private float _xAxisClamp = 0;

        private bool _canRotate = true;
        private const float BaseRatio = 1f;

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
                case > 90:
                    _xAxisClamp = 90;
                    rotation.x = 90;
                    break;
                case < -90:
                    _xAxisClamp = -90;
                    rotation.x = -90;
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