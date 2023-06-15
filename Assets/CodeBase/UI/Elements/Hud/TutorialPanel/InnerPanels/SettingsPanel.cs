using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel.InnerPanels
{
    public class SettingsPanel : IconsPanel
    {
        [SerializeField] private Action _esc;

        public override void ShowForPc()
        {
            _esc.gameObject.SetActive(true);
        }

        public override void ShowForMobile()
        {
            _esc.gameObject.SetActive(false);
        }
    }
}