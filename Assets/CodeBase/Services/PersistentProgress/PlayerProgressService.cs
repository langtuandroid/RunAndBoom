using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress Progress { get; private set; }
        public string CurrentError { get; set; }

        public void ClearProgress() =>
            Progress = null;

        public void SetProgress(PlayerProgress progress) =>
            Progress = progress;
    }
}