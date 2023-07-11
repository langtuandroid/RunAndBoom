using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Look : IconsPanel
    {
        [SerializeField] private Action _lookMouse;
        [SerializeField] private Action _lookJoustick;

        public override void ShowForPc()
        {
            _lookMouse.Show();
            _lookJoustick.Hide();
        }

        public override void ShowForMobile()
        {
            _lookJoustick.Show();
            _lookMouse.Hide();
        }

        protected override void RuChosen()
        {
            _lookJoustick.Text.text = LocalizationConstants.TutorialLookJoystickRu;
            _lookMouse.Text.text = LocalizationConstants.TutorialLookMouseRu;
        }

        protected override void TrChosen()
        {
            _lookJoustick.Text.text = LocalizationConstants.TutorialLookJoystickTr;
            _lookMouse.Text.text = LocalizationConstants.TutorialLookMouseTr;
        }

        protected override void EnChosen()
        {
            _lookJoustick.Text.text = LocalizationConstants.TutorialLookJoystickEn;
            _lookMouse.Text.text = LocalizationConstants.TutorialLookMouseEn;
        }
    }
}