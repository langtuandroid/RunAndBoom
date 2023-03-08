using CodeBase.Data;
using CodeBase.UI.Elements;

namespace CodeBase.UI.Screens.Level
{
    public class LevelSceneOpenButton : SceneOpenButton
    {
        protected override string Scene => Scenes.Level1;

        protected override bool Checked
        {
            get { return true; }
        }
    }
}