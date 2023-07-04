using CodeBase.Services;
using CodeBase.Services.Input;
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

        private bool _show;
        private IInputService _inputService;

        protected override void InitiateServices() =>
            _inputService = AllServices.Container.Single<IInputService>();

        private void Update()
        {
            if (!_show)
                return;

            if (_inputService.MoveAxis.magnitude > Constants.Epsilon)
            {
                Hide();
                _show = false;
            }
        }

        public override void ShowForPc()
        {
            _moveForward.Show();
            _moveBack.Show();
            _moveLeft.Show();
            _moveRight.Show();
            _moveJoystick.Hide();
            Show();
            _show = true;
        }

        public override void ShowForMobile()
        {
            _moveForward.Hide();
            _moveBack.Hide();
            _moveLeft.Hide();
            _moveRight.Hide();
            _moveJoystick.Show();
            Show();
            _show = true;
        }

        protected override void RuChosen()
        {
            _moveForward.Text.text = LocalizationConstants.TutorialForwardButtonRu;
            _moveBack.Text.text = LocalizationConstants.TutorialBackButtonRu;
            _moveLeft.Text.text = LocalizationConstants.TutorialLeftButtonRu;
            _moveRight.Text.text = LocalizationConstants.TutorialRightButtonRu;
            _moveJoystick.Text.text = LocalizationConstants.TutorialMoveJoystickRu;
        }

        protected override void TrChosen()
        {
            _moveForward.Text.text = LocalizationConstants.TutorialForwardButtonTr;
            _moveBack.Text.text = LocalizationConstants.TutorialBackButtonTr;
            _moveLeft.Text.text = LocalizationConstants.TutorialLeftButtonTr;
            _moveRight.Text.text = LocalizationConstants.TutorialRightButtonTr;
            _moveJoystick.Text.text = LocalizationConstants.TutorialMoveJoystickTr;
        }

        protected override void EnChosen()
        {
            _moveForward.Text.text = LocalizationConstants.TutorialForwardButtonEn;
            _moveBack.Text.text = LocalizationConstants.TutorialBackButtonEn;
            _moveLeft.Text.text = LocalizationConstants.TutorialLeftButtonEn;
            _moveRight.Text.text = LocalizationConstants.TutorialRightButtonEn;
            _moveJoystick.Text.text = LocalizationConstants.TutorialMoveJoystickEn;
        }
    }
}