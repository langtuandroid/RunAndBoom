using CodeBase.Services;
using CodeBase.Services.PlayerAuthorization;
using UnityEngine;

namespace CodeBase.Hero
{
    public class ServiceAuthorization : MonoBehaviour
    {
        private IAuthorization _authorization;

        private void Awake()
        {
            if (Application.isEditor)
                return;

            _authorization = AllServices.Container.Single<IAuthorization>();
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
        }

        private void Start()
        {
            if (Application.isEditor)
                return;

            Authorize();
        }

        private void Authorize()
        {
            if (_authorization.IsAuthorized())
                _authorization.RequestPersonalProfileDataPermission();
            else
                _authorization.Authorize();
        }

        private void RequestPersonalProfileDataPermission() =>
            _authorization.RequestPersonalProfileDataPermission();

        private void ShowError(string error) =>
            Debug.Log($"Error: {error}");
    }
}