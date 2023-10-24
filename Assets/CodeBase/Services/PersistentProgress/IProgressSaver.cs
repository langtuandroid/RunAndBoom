using CodeBase.Data.Progress;

namespace CodeBase.Services.PersistentProgress
{
    public interface IProgressSaver : IProgressReader
    {
        void UpdateProgressData(ProgressData progressData);
    }
}