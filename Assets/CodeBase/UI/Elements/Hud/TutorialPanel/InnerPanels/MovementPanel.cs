using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class MovementPanel : IconsPanel
    {
        [SerializeField] private Action _moveForward;
        [SerializeField] private Action _moveBack;
        [SerializeField] private Action _moveLeft;
        [SerializeField] private Action _moveRight;
        [SerializeField] private Action _moveJoystick;

        public override void ShowForPc()
        {
            _moveForward.gameObject.SetActive(true);
            _moveBack.gameObject.SetActive(true);
            _moveLeft.gameObject.SetActive(true);
            _moveRight.gameObject.SetActive(true);
            _moveJoystick.gameObject.SetActive(false);
        }

        public override void ShowForMobile()
        {
            _moveForward.gameObject.SetActive(false);
            _moveBack.gameObject.SetActive(false);
            _moveLeft.gameObject.SetActive(false);
            _moveRight.gameObject.SetActive(false);
            _moveJoystick.gameObject.SetActive(true);
        }
    }
}