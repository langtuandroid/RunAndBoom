using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class ShootPanel : IconsPanel
    {
        [SerializeField] private Action _lmbClick;
        [SerializeField] private Action _screenClick;

        public override void ShowForPc()
        {
            _lmbClick.gameObject.SetActive(true);
            _screenClick.gameObject.SetActive(false);
        }

        public override void ShowForMobile()
        {
            _screenClick.gameObject.SetActive(true);
            _lmbClick.gameObject.SetActive(false);
        }
    }
}