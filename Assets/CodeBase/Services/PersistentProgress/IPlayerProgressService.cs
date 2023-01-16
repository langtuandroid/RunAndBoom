using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPlayerProgressService : IService
    {
        PlayerProgress Progress { get; set; }

        string CurrentError { get; set; }
    }
}