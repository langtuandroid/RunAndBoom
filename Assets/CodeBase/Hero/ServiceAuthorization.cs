using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.PlayerAuthorization;
using UnityEngine;

namespace CodeBase.Hero
{
    public class ServiceAuthorization : MonoBehaviour
    {
        private IAdsService _adsService;
        private IAuthorization _authorization;

        private void OnEnable()
        {
            if (Application.isEditor)
                return;

            if (_adsService == null)
                _adsService = AllServices.Container.Single<IAdsService>();

            if (_authorization == null)
                _authorization = AllServices.Container.Single<IAuthorization>();

            _adsService.OnInitializeSuccess += TryAuthorize;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            if (_adsService != null)
                _adsService.OnInitializeSuccess -= TryAuthorize;
        }

        private void InitializeAdsSDK()
        {
            Debug.Log("ServiceAuthorization InitializeAdsSDK");
            if (_adsService.IsInitialized())
                TryAuthorize();
            else
                StartCoroutine(_adsService.Initialize());
        }

        private void TryAuthorize()
        {
            Debug.Log("ServiceAuthorization InitializeAdsSDK");
            if (_authorization.IsAuthorized())
            {
                _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
                RequestPersonalProfileDataPermission();
            }
            else
            {
                Authorize();
            }
        }

        private void Authorize()
        {
            Debug.Log("ServiceAuthorization Authorize");
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnAuthorizeErrorCallback += ShowAuthorizeError;
            _authorization.Authorize();
        }

        private void RequestPersonalProfileDataPermission()
        {
            Debug.Log("ServiceAuthorization RequestPersonalProfileDataPermission");
            _authorization.OnAuthorizeSuccessCallback -= RequestPersonalProfileDataPermission;
            _authorization.OnRequestErrorCallback += ShowRequestError;
            _authorization.RequestPersonalProfileDataPermission();
        }

        private void ShowAuthorizeError(string error)
        {
            Debug.Log($"ServiceAuthorization ShowAuthorizeError {error}");
            _authorization.OnAuthorizeErrorCallback -= ShowAuthorizeError;
        }

        private void ShowRequestError(string error)
        {
            Debug.Log($"ServiceAuthorization ShowRequestError {error}");
            _authorization.OnRequestErrorCallback -= ShowRequestError;
        }
    }
}