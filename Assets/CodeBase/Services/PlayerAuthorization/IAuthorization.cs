using System;
using Agava.YandexGames;

namespace CodeBase.Services.PlayerAuthorization
{
    public interface IAuthorization : IService
    {
        bool IsAuthorized();
        void Authorize();
        void RequestPersonalProfileDataPermission();
        void GetProfileData();
        void GetPlayerData();
        void SetPlayerData(string data);
        event Action OnAuthorizeSuccessCallback;
        event Action OnRequestPersonalProfileDataPermissionSuccessCallback;
        event Action<PlayerAccountProfileDataResponse> OnGetProfileDataSuccessCallback;
        event Action<string> OnGetPlayerDataSuccessCallback;
        event Action OnSetPlayerDataSuccessCallback;
        event Action<string> OnErrorCallback;
    }
}