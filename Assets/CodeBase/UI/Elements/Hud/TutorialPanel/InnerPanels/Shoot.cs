using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Shoot : IconsPanel
    {
        [SerializeField] private Action _lmbClick;
        [SerializeField] private Action _buttonClick;

        private bool _show;
        private IInputService _inputService;

        protected override void InitiateServices() =>
            _inputService = AllServices.Container.Single<IInputService>();

        private void Update()
        {
            if (!_show)
                return;

            if (_inputService.IsAttackButtonUp())
            {
                _lmbClick.Hide();
                _buttonClick.Hide();
                _show = false;
            }
        }

        public override void ShowForPc()
        {
            _lmbClick.Show();
            _buttonClick.Hide();
            _show = true;
        }

        public override void ShowForMobile()
        {
            _buttonClick.Show();
            _lmbClick.Hide();
            _show = true;
        }

        protected override void RuChosen()
        {
            _lmbClick.Text.text = LocalizationConstants.TutorialLmbRu;
            _buttonClick.Text.text = LocalizationConstants.TutorialShootButtonRu;
        }

        protected override void TrChosen()
        {
            _lmbClick.Text.text = LocalizationConstants.TutorialLmbTr;
            _buttonClick.Text.text = LocalizationConstants.TutorialShootButtonTr;
        }

        protected override void EnChosen()
        {
            _lmbClick.Text.text = LocalizationConstants.TutorialLmbEn;
            _buttonClick.Text.text = LocalizationConstants.TutorialShootButtonEn;
        }
    }
}