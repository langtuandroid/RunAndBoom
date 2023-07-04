using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Settings : IconsPanel
    {
        [SerializeField] private Action _esc;

        private bool _show;

        private void Update()
        {
            if (!_show)
                return;

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Hide();
                _show = false;
            }
        }

        public override void ShowForPc()
        {
            Show();
            _show = true;
        }

        public override void ShowForMobile() =>
            Hide();

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