using System.Collections;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.PlayerAuthorization;
using UnityEngine;

namespace CodeBase.Hero
{
    public class ServiceAuthorization : MonoBehaviour
    {
        private IAuthorization _authorization;
        private IAdsService _adsService;

        private void Start()
        {
            if (Application.isEditor)
                return;

            _authorization = AllServices.Container.Single<IAuthorization>();
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _adsService = AllServices.Container.Single<IAdsService>();
            _adsService.OnInitializeSuccess += TryAuthorize;
            InitializeAdsSDK();
        }

        private void InitializeAdsSDK()
        {
            Debug.Log("InitializeAdsSDK");
            if (_adsService.IsInitialized())
                TryAuthorize();
            else
                StartCoroutine(CoroutineInitializeAdsSDK());
        }

        private IEnumerator CoroutineInitializeAdsSDK()
        {
            Debug.Log("CoroutineInitializeAdsSDK");
            yield return _adsService.Initialize();
        }

        private void TryAuthorize()
        {
            Debug.Log("TryAuthorize");
            if (_authorization.IsAuthorized())
                RequestPersonalProfileDataPermission();
            else
                Authorize();
        }

        private void Authorize()
        {
            Debug.Log("Authorize");
            _authorization.OnAuthorizeSuccessCallback += _authorization.RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _authorization.Authorize();
        }

        private void RequestPersonalProfileDataPermission()
        {
            Debug.Log("RequestPersonalProfileDataPermission");
            _authorization.RequestPersonalProfileDataPermission();
            _authorization.OnAuthorizeSuccessCallback -= RequestPersonalProfileDataPermission;
        }

        private void ShowError(string error)
        {
            Debug.Log($"ServiceAuthorization ShowError {error}");
            _authorization.OnErrorCallback -= ShowError;
        }
    }
}