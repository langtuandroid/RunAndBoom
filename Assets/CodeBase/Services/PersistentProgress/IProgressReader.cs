using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}