using CodeBase.Data.Progress;

namespace CodeBase.Services.PersistentProgress
{
    public interface IProgressReader
    {
        void LoadProgressData(ProgressData progressData);
    }
}