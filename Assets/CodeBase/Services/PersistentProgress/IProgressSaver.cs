using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IProgressSaver : IProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}