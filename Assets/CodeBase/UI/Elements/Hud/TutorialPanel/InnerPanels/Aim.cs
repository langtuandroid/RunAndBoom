using CodeBase.Services.Localization;
using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class Aim : IconsPanel
    {
        [SerializeField] private Action _swipe;

        public override void ShowForPc()
        {
            // _swipe.gameObject.SetActive(false);
            Hide();
        }

        public override void ShowForMobile()
        {
            // _swipe.gameObject.SetActive(true);
            Show();
        }

        protected override void RuChosen() =>
            _swipe.Text.text = LocalizationConstants.TutorialSwipeRu;

        protected override void TrChosen() =>
            _swipe.Text.text = LocalizationConstants.TutorialSwipeTr;

        protected override void EnChosen() =>
            _swipe.Text.text = LocalizationConstants.TutorialSwipeEn;
    }
}