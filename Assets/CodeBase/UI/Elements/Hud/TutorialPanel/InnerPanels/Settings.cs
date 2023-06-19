using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Settings : IconsPanel
    {
        [SerializeField] private Action _esc;

        public override void ShowForPc()
        {
            // _esc.Show();
            Show();
        }

        public override void ShowForMobile()
        {
            // _esc.Hide();
            Hide();
        }

        protected override void RuChosen()
        {
        }

        protected override void TrChosen()
        {
        }

        protected override void EnChosen()
        {
        }
    }
}