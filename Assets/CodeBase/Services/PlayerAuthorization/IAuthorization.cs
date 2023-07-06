using System;
using Agava.YandexGames;

namespace CodeBase.Services.PlayerAuthorization
{
    public interface IAuthorization : IService
    {
        event Action OnAuthorizeSuccessCallback;
        event Action OnRequestPersonalProfileDataPermissionSuccessCallback;
        event Action<PlayerAccountProfileDataResponse> OnGetProfileDataSuccessCallback;
        event Action<string> OnGetPlayerDataSuccessCallback;
        event Action OnSetPlayerDataSuccessCallback;
        event Action<string> OnErrorCallback;
        
        bool IsAuthorized();
        void Authorize();
        void RequestPersonalProfileDataPermission();
        void GetProfileData();
        void GetPlayerData();
        void SetPlayerData(string data);
    }
}