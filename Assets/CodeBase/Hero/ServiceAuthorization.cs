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

        private void Awake()
        {
            if (Application.isEditor)
                return;

            _authorization = AllServices.Container.Single<IAuthorization>();
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _adsService = AllServices.Container.Single<IAdsService>();
        }

        private void Start()
        {
            if (Application.isEditor)
                return;

            Authorize();
            InitializeAdsSDK();
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

        private void InitializeAdsSDK()
        {
            if (IsAdsSDKInitialized())
                StartCoroutine(CoroutineInitializeAdsSDK());
        }

        private bool IsAdsSDKInitialized() =>
            _adsService.IsInitialized();

        private IEnumerator CoroutineInitializeAdsSDK()
        {
            yield return _adsService.Initialize();
        }

        private void ShowError(string error) =>
            Debug.Log($"Error: {error}");
    }
}