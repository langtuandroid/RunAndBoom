using Agava.YandexGames;

namespace CodeBase.Services.GameReadyService
{
    public class YandexGameReadyService : IGameReadyService
    {
        public void OnGameReadyButtonClick() =>
            YandexGamesSdk.GameReady();
    }
}