using Agava.YandexGames;

namespace CodeBase.Services.Ads
{
    public class YandexAuthorization
    {
        public void Authorize()
        {
            if (!PlayerAccount.IsAuthorized)
            {
            }
            else
            {
                if (PlayerAccount.HasPersonalProfileDataPermission)
                    PlayerAccount.RequestPersonalProfileDataPermission();

                // PlayerAccount.GetProfileData();
            }
        }
    }
}