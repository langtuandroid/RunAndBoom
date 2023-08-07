using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Shoot : IconsPanel
    {
        [SerializeField] private Action _lmbClick;
        [SerializeField] private Action _buttonClick;

        public override void ShowForPc()
        {
            _lmbClick.Show();
            _buttonClick.Hide();
        }

        public override void ShowForMobile()
        {
            _buttonClick.Show();
            _lmbClick.Hide();
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