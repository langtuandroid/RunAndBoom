using UnityEngine;

namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public abstract class IconsPanel : MonoBehaviour
    {
        public abstract void ShowForPc();

        public abstract void ShowForMobile();

        public void Hide() =>
            gameObject.SetActive(false);
    }
}