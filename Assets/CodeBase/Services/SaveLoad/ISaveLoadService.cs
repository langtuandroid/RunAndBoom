using CodeBase.Data;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        void ClearProgress();
        PlayerProgress LoadProgress();
    }
}