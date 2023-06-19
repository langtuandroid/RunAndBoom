using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Movement : IconsPanel
    {
        [SerializeField] private Action _moveForward;
        [SerializeField] private Action _moveBack;
        [SerializeField] private Action _moveLeft;
        [SerializeField] private Action _moveRight;
        [SerializeField] private Action _moveJoystick;

        public override void ShowForPc()
        {
            _moveForward.Show();
            _moveBack.Show();
            _moveLeft.Show();
            _moveRight.Show();
            _moveJoystick.Hide();
            Show();
        }

        public override void ShowForMobile()
        {
            _moveForward.Hide();
            _moveBack.Hide();
            _moveLeft.Hide();
            _moveRight.Hide();
            _moveJoystick.Show();
            Show();
        }

        protected override void RuChosen()
        {
            _moveForward.Text.text = LocalizationConstants.TutorialForwardButtonRu;
            _moveBack.Text.text = LocalizationConstants.TutorialBackButtonRu;
            _moveLeft.Text.text = LocalizationConstants.TutorialLeftButtonRu;
            _moveRight.Text.text = LocalizationConstants.TutorialRightButtonRu;
            _moveJoystick.Text.text = LocalizationConstants.TutorialJoystickRu;
        }

        protected override void TrChosen()
        {
            _moveForward.Text.text = LocalizationConstants.TutorialForwardButtonTr;
            _moveBack.Text.text = LocalizationConstants.TutorialBackButtonTr;
            _moveLeft.Text.text = LocalizationConstants.TutorialLeftButtonTr;
            _moveRight.Text.text = LocalizationConstants.TutorialRightButtonTr;
            _moveJoystick.Text.text = LocalizationConstants.TutorialJoystickTr;
        }

        protected override void EnChosen()
        {
            _moveForward.Text.text = LocalizationConstants.TutorialForwardButtonEn;
            _moveBack.Text.text = LocalizationConstants.TutorialBackButtonEn;
            _moveLeft.Text.text = LocalizationConstants.TutorialLeftButtonEn;
            _moveRight.Text.text = LocalizationConstants.TutorialRightButtonEn;
            _moveJoystick.Text.text = LocalizationConstants.TutorialJoystickEn;
        }
    }
}