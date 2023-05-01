using CodeBase.UI.Elements;

namespace CodeBase.UI.Screens.Level
{
    public class LevelSceneOpenButton : SceneOpenButton
    {
        protected override string Scene => Data.Scene.Level1;

        protected override bool Checked
        {
            get { return true; }
        }
    }
}