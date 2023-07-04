using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        [FormerlySerializedAs("_aim")] [SerializeField]
        private Look look;

        [SerializeField] private Movement _movement;
        [SerializeField] private Shoot _shoot;
        [SerializeField] private InnerPanels.Weapons _weapons;

        private bool _isMovedForward;
        private bool _isMovedBack;
        private bool _isMovedLeft;
        private bool _isMovedRight;
        private bool _isShot;
        private bool _isWeaponSelected;

        private void OnEnable()
        {
            if (AllServices.Container.Single<IInputService>() is MobileInputService)
            {
                look.ShowForMobile();
                _settings.ShowForMobile();
                _movement.ShowForMobile();
                _shoot.ShowForMobile();
                _weapons.ShowForMobile();
            }
            else
            {
                look.ShowForPc();
                _settings.ShowForPc();
                _movement.ShowForPc();
                _shoot.ShowForPc();
                _weapons.ShowForPc();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _isMovedForward = true;
                CheckMovement();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _isMovedBack = true;
                CheckMovement();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _isMovedLeft = true;
                CheckMovement();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _isMovedRight = true;
                CheckMovement();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
                _weapons.Hide();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                _shoot.Hide();

            if (Input.GetKeyDown(KeyCode.Escape))
                _settings.Hide();
        }

        private void CheckMovement()
        {
            if (_isMovedForward && _isMovedBack && _isMovedLeft && _isMovedRight)
                _movement.Hide();
        }
    }
}