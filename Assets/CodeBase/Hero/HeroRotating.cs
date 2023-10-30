using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements.Hud.MobileInputPanel.Joysticks;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroRotating : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _desktopVerticalSensitivity = 0.2f;
        [SerializeField] private float _desktopHorizontalSensitivity = 10.0f;
        [SerializeField] private float _mobileVerticalSensitivity = 5.0f;
        [SerializeField] private float _mobileHorizontalSensitivity = 100.0f;
        [SerializeField] private float _edgeAngle = 85f;

        private IInputService _inputService;
        private IPlayerProgressService _playerProgressService;
        private LookJoystick _lookJoystick;
        private bool _isMobile;
        private bool _update;
        private float _xAxisClamp = 0;
        private bool _canRotate = true;
        private float _verticalRotation;
        private float _verticalAngle;
        private Vector2 _lookInput = Vector3.zero;
        private float _verticalSensitivity;
        private float _horizontalSensitivity;

        public void ConstructDesktopPlatform(IInputService inputService, IPlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _inputService = inputService;
            _isMobile = false;
            _update = true;
            _inputService.Looked += DesktopRotate;
            _playerProgressService.SettingsData.AimVerticalSensitiveMultiplierChanged +=
                UpdateVerticalSensitiveMultiplierMultiplier;
            _playerProgressService.SettingsData.AimHorizontalSensitiveMultiplierChanged +=
                UpdateHorizontalSensitiveMultiplierMultiplier;
            UpdateVerticalSensitiveMultiplierMultiplier();
            UpdateHorizontalSensitiveMultiplierMultiplier();
        }

        public void ConstructMobilePlatform(LookJoystick lookJoystick, IPlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _lookJoystick = lookJoystick;
            _isMobile = true;
            _update = true;
            _playerProgressService.SettingsData.AimVerticalSensitiveMultiplierChanged +=
                UpdateVerticalSensitiveMultiplierMultiplier;
            _playerProgressService.SettingsData.AimHorizontalSensitiveMultiplierChanged +=
                UpdateHorizontalSensitiveMultiplierMultiplier;
            UpdateVerticalSensitiveMultiplierMultiplier();
            UpdateHorizontalSensitiveMultiplierMultiplier();
        }

        private void DesktopRotate(Vector2 lookInput)
        {
            _lookInput = lookInput;
            // RotateVertical();
            // RotateHorizontal();
        }

        private void UpdateVerticalSensitiveMultiplierMultiplier()
        {
            if (_isMobile)
                _verticalSensitivity = _playerProgressService.SettingsData.AimVerticalSensitiveMultiplier *
                                       _mobileVerticalSensitivity;
            else
                _verticalSensitivity = _playerProgressService.SettingsData.AimVerticalSensitiveMultiplier *
                                       _desktopVerticalSensitivity;
        }

        private void UpdateHorizontalSensitiveMultiplierMultiplier()
        {
            if (_isMobile)
                _horizontalSensitivity = _playerProgressService.SettingsData.AimHorizontalSensitiveMultiplier *
                                         _mobileHorizontalSensitivity;
            else
                _horizontalSensitivity = _playerProgressService.SettingsData.AimHorizontalSensitiveMultiplier *
                                         _desktopHorizontalSensitivity;
        }

        private void Start()
        {
            if (_inputService is DesktopInputService)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            if (_update == false)
                return;

            if (!_canRotate)
                return;

            if (_isMobile)
                MobileRotate();
            else
                DesktopRotate();
        }

        private void DesktopRotate()
        {
            transform.Rotate(Vector3.up * _lookInput.x * _horizontalSensitivity * Time.deltaTime);
            _verticalRotation -= _lookInput.y;
            ClampAngle();
            _camera.transform.localRotation = Quaternion.Euler(_verticalRotation
                                                               // * _desktopVerticalSensitivity
                                                               * _verticalSensitivity
                , 0, 0);
        }

        private void ClampAngle()
        {
            // if (_isMobile)
            //     _verticalAngle = _edgeAngle
            //                      / _verticalSensitivity
            //         ;
            // else
            _verticalAngle = _edgeAngle
                             / _verticalSensitivity
                ;

            _verticalRotation = Mathf.Clamp(_verticalRotation
                // * _verticalSensitivity
                , -_verticalAngle, _verticalAngle);
        }

        private void MobileRotate()
        {
            RotateHorizontal();
            RotateVertical();
        }

        private void RotateVertical()
        {
            if (_lookJoystick.Input.sqrMagnitude > Constants.RotationEpsilon)
                _verticalRotation -= _lookJoystick.Input.y;

            ClampAngle();
            _camera.transform.localRotation = Quaternion.Euler(_verticalRotation
                // * _verticalSensitivity
                , 0, 0);
        }

        private void RotateHorizontal()
        {
            if (_lookJoystick.Input.sqrMagnitude > Constants.RotationEpsilon)
                transform.Rotate(Vector3.up * _lookJoystick.Input.x * _horizontalSensitivity * Time.deltaTime);

            // transform.Rotate(Vector3.up * Constants.Zero * _horizontalSensitivity * Time.deltaTime);
        }

        // private void DesktopRotate()
        // {
        //     TryRotateVertical();
        //     RotateHorizontal();
        // }
        //
        // private void MobileRotate()
        // {
        //     TryRotateVertical();
        //     RotateHorizontal();
        // }
        //
        // private void TryRotateVertical()
        // {
        //     if (_isMobile)
        //     {
        //         if (_lookJoystick.Input.sqrMagnitude > Constants.RotationEpsilon)
        //             _verticalRotation -= _lookJoystick.Input.y;
        //     }
        //     else
        //     {
        //         _verticalRotation -= _lookInput.y;
        //     }
        //
        //     Debug.Log($"TryRotateVertical _verticalRotation {_verticalRotation}");
        //     RotateVertical();
        // }
        //
        // private void RotateVertical()
        // {
        //     ClampAngle();
        //     _camera.transform.localRotation =
        //         Quaternion.Euler(_verticalRotation, 0, 0);
        // }
        //
        // private void ClampAngle()
        // {
        //     // if (_isMobile)
        //     //     _verticalAngle = _edgeAngle / (_mobileVerticalSensitivity * _verticalSensitiveMultiplier);
        //     // else
        //     //     _verticalAngle = _edgeAngle / (_desktopVerticalSensitivity * _verticalSensitiveMultiplier);
        //     //
        //     // Debug.Log($"_verticalAngle {_verticalAngle}");
        //     // _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalAngle, _verticalAngle);
        //
        //     if (_isMobile)
        //         _verticalRotation = Mathf.Clamp(
        //             _verticalRotation * _mobileVerticalSensitivity * _verticalSensitiveMultiplier,
        //             -_edgeAngle / (_mobileVerticalSensitivity * _verticalSensitiveMultiplier),
        //             _edgeAngle / _mobileVerticalSensitivity * _verticalSensitiveMultiplier);
        //     else
        //         _verticalRotation =
        //             Mathf.Clamp(
        //                 _verticalRotation * _desktopVerticalSensitivity * _verticalSensitiveMultiplier,
        //                 -_edgeAngle / (_desktopVerticalSensitivity * _verticalSensitiveMultiplier),
        //                 _edgeAngle / (_desktopVerticalSensitivity * _verticalSensitiveMultiplier));
        //
        //     Debug.Log($"ClampAngle _verticalRotation {_verticalRotation}");
        // }
        //
        // private void RotateHorizontal()
        // {
        //     if (_isMobile)
        //     {
        //         if (_lookJoystick.Input.sqrMagnitude > Constants.RotationEpsilon)
        //             transform.Rotate(Vector3.up
        //                              * _lookJoystick.Input.x
        //                              * _mobileHorizontalSensitivity
        //                              * _horizontalSensitiveMultiplier
        //                              * Time.deltaTime);
        //     }
        //     else
        //     {
        //         transform.Rotate(Vector3.up
        //                          * _lookInput.x
        //                          * _desktopHorizontalSensitivity
        //                          * _horizontalSensitiveMultiplier
        //                          * Time.deltaTime);
        //     }
        // }

        public void TurnOn() =>
            _canRotate = true;

        public void TurnOff()
        {
            _canRotate = false;
            transform.Rotate(Vector3.zero);
        }
    }
}