using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress Progress { get; set; }
        public string CurrentError { get; set; }
    }
}