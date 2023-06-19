namespace CodeBase.UI.Elements.Hud.TutorialPanel
{
    public abstract class IconsPanel : BaseText
    {
        public abstract void ShowForPc();

        public abstract void ShowForMobile();

        public void Hide() =>
            gameObject.SetActive(false);

        public void Show() =>
            gameObject.SetActive(true);
    }
}