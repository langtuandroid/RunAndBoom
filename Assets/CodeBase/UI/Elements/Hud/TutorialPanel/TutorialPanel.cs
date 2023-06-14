using CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private MovementPanel _movementPanel;
        [SerializeField] private InnerPanels.WeaponsPanel _weaponsPanel;
        [SerializeField] private ShootPanel _shootPanel;

        private bool _isMovedForward;
        private bool _isMovedBack;
        private bool _isMovedLeft;
        private bool _isMovedRight;
        private bool _isShot;
        private bool _isWeaponSelected;

        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                _movementPanel.ShowForMobile();
                _shootPanel.ShowForMobile();
                _weaponsPanel.ShowForMobile();
            }
            else
            {
                _movementPanel.ShowForPc();
                _shootPanel.ShowForPc();
                _weaponsPanel.ShowForPc();
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
                _weaponsPanel.Hide();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                _shootPanel.Hide();
        }

        private void CheckMovement()
        {
            if (_isMovedForward && _isMovedBack && _isMovedLeft && _isMovedRight)
                _movementPanel.Hide();
        }
    }
}