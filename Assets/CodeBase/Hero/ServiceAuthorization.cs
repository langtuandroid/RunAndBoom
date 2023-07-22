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

            InitializeAdsSDK();
        }

        private void InitializeAdsSDK()
        {
            Debug.Log("ServiceAuthorization InitializeAdsSDK");
            if (!_adsService.IsInitialized())
                StartCoroutine(_adsService.Initialize());
        }
    }
}