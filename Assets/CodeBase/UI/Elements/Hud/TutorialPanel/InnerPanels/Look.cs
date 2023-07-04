using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Look : IconsPanel
    {
        [SerializeField] private Action _lookJoystick;

        private bool _show;
        private IInputService _inputService;

        protected override void InitiateServices() =>
            _inputService = AllServices.Container.Single<IInputService>();

        private void Update()
        {
            if (!_show)
                return;

            if (_inputService.LookAxis.magnitude > Constants.Epsilon)
            {
                Hide();
                _show = false;
            }
        }

        public override void ShowForPc()
        {
            Hide();
            _show = false;
        }

        public override void ShowForMobile()
        {
            Show();
            _show = true;
        }

        protected override void RuChosen() =>
            _lookJoystick.Text.text = LocalizationConstants.TutorialLookJoystickRu;

        protected override void TrChosen() =>
            _lookJoystick.Text.text = LocalizationConstants.TutorialLookJoystickTr;

        protected override void EnChosen() =>
            _lookJoystick.Text.text = LocalizationConstants.TutorialLookJoystickEn;
    }
}