using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPlayerProgressService : IService
    {
        public PlayerProgress Progress { get; }

        string CurrentError { get; set; }
        void ClearProgress();
        void SetProgress(PlayerProgress progress);
    }
}