﻿using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.PlayerAuthorization;
using UnityEngine;

namespace CodeBase.Hero
{
    public class ServiceAuthorization : MonoBehaviour
    {
        private IAdsService _adsService;
        private IAuthorization _authorization;

        private void Start()
        {
            if (Application.isEditor)
                return;

            _adsService = AllServices.Container.Single<IAdsService>();
            _authorization = AllServices.Container.Single<IAuthorization>();
        }

        private void OnEnable()
        {
            _adsService.OnInitializeSuccess += TryAuthorize;
            InitializeAdsSDK();
        }

        private void OnDisable()
        {
            _adsService.OnInitializeSuccess -= TryAuthorize;
        }

        private void InitializeAdsSDK()
        {
            if (_adsService.IsInitialized())
                TryAuthorize();
            else
                StartCoroutine(_adsService.Initialize());
        }

        private void TryAuthorize()
        {
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
            Debug.Log("Authorize");
            _authorization.OnAuthorizeSuccessCallback += RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _authorization.Authorize();
        }

        private void RequestPersonalProfileDataPermission()
        {
            Debug.Log("RequestPersonalProfileDataPermission");
            _authorization.OnAuthorizeSuccessCallback -= RequestPersonalProfileDataPermission;
            _authorization.OnErrorCallback += ShowError;
            _authorization.RequestPersonalProfileDataPermission();
        }

        private void ShowError(string error)
        {
            Debug.Log($"Show error {error}");
            _authorization.OnErrorCallback -= ShowError;
        }
    }
}