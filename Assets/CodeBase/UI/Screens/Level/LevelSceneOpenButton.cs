using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using Zenject;

namespace CodeBase.UI.Screens.Level
{
    public class LevelSceneOpenButton : SceneOpenButton
    {
        protected override string Scene => Scenes.Level1;

        protected override bool Checked
        {
            get
            {
                if (ProgressService.Progress.SelectedWeaponTypeIds.Count == 0)
                {
                    ProgressService.CurrentError = Errors.WeaponNotChosen;
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IPlayerProgressService progressService, IWindowService windowService,
            ISaveLoadService saveLoadService)
        {
            base.Construct(stateMachine, progressService, windowService, saveLoadService);
        }
    }
}