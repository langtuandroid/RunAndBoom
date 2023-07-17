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

            _adsService = AllServices.Container.Single<IAdsService>();
            InitializeAdsSDK();
        }

        private void InitializeAdsSDK()
        {
            if (!_adsService.IsInitialized())
                StartCoroutine(_adsService.Initialize());
        }
    }
}