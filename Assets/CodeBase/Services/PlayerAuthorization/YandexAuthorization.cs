using System;
using Agava.YandexGames;

namespace CodeBase.Services.PlayerAuthorization
{
    public class YandexAuthorization : IAuthorization
    {
        public event Action OnAuthorizeSuccessCallback;
        public event Action OnRequestPersonalProfileDataPermissionSuccessCallback;
        public event Action<PlayerAccountProfileDataResponse> OnGetProfileDataSuccessCallback;
        public event Action<string> OnGetPlayerDataSuccessCallback;
        public event Action OnSetPlayerDataSuccessCallback;
        public event Action<string> OnErrorCallback;

        public bool IsAuthorized() =>
            PlayerAccount.IsAuthorized;

        public void Authorize() =>
            PlayerAccount.Authorize(OnAuthorizeSuccessCallback, OnErrorCallback);

        public void RequestPersonalProfileDataPermission() =>
            PlayerAccount.RequestPersonalProfileDataPermission(
                OnRequestPersonalProfileDataPermissionSuccessCallback,
                OnErrorCallback);

        public void GetProfileData() =>
            PlayerAccount.GetProfileData(OnGetProfileDataSuccessCallback, OnErrorCallback);

        public void GetPlayerData() =>
            PlayerAccount.GetPlayerData(OnGetPlayerDataSuccessCallback, OnErrorCallback);

        public void SetPlayerData(string data) =>
            PlayerAccount.SetPlayerData(data, OnSetPlayerDataSuccessCallback, OnErrorCallback);
    }
}