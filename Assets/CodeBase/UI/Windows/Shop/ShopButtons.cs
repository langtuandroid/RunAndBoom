using System;
using CodeBase.Services.Audio;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopButtons : MonoBehaviour
    {
        // [SerializeField] private AudioSource _audioGenerate;
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private Button _refreshWithAdsButton;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private ItemsGeneratorBase _generator;

        private int _currentRefreshCount = 0;
        private int _maxRefreshCount;
        private int _watchAdsNumber;
        private float _currentVolume = 1f;

        private void Awake()
        {
            _skipButton.onClick.AddListener(CloseShop);
            _refreshButton.onClick.AddListener(GenerateShopItems);
            _refreshWithAdsButton.onClick.AddListener(ShowAdsAndGenerate);
            _generator.GenerationStarted += DisableRefreshButtons;
            _generator.GenerationEnded += CheckRefreshButtons;
        }

        public void Construct(int maxRefreshCount, int watchAdsNumber)
        {
            _maxRefreshCount = maxRefreshCount;
            _watchAdsNumber = watchAdsNumber;
            CheckCounts();
        }

        private void DisableRefreshButtons()
        {
            _refreshButton.enabled = false;
            _refreshWithAdsButton.enabled = false;
        }

        private void CheckRefreshButtons()
        {
            _refreshButton.enabled = true;
            _refreshWithAdsButton.enabled = true;

            _currentRefreshCount++;
            CheckCounts();
        }

        private void CheckCounts()
        {
            if (NeedShowRefreshButtons())
            {
                CheckCurrentEqualsWatchAdsNumber();
            }
            else
            {
                HideRefreshButton();
                HideRefreshWithAdsButton();
            }
        }

        private bool NeedShowRefreshButtons()
        {
            if (_maxRefreshCount == Decimal.Zero || _maxRefreshCount == _currentRefreshCount)
            {
                return false;
            }

            return true;
        }

        private void CheckCurrentEqualsWatchAdsNumber()
        {
            if (_watchAdsNumber == _currentRefreshCount)
            {
                ShowRefreshWithAdsButton();
                HideRefreshButton();
            }
            else
            {
                ShowRefreshButton();
                HideRefreshWithAdsButton();
            }
        }

        private void ShowRefreshButton()
        {
            _refreshButton.gameObject.SetActive(true);
        }

        private void HideRefreshButton()
        {
            _refreshButton.gameObject.SetActive(false);
        }

        private void ShowRefreshWithAdsButton()
        {
            _refreshWithAdsButton.gameObject.SetActive(true);
        }

        private void HideRefreshWithAdsButton()
        {
            _refreshWithAdsButton.gameObject.SetActive(false);
        }

        private void Start() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void CloseShop()
        {
            _shopWindow.Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ShowAdsAndGenerate()
        {
            //TODO ShowAds screen
            GenerateShopItems();
        }

        private void GenerateShopItems()
        {
            // _audioGenerate.Play();
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.MenuOpen), transform: transform,
                _currentVolume);
            _generator.Generate();
        }
    }
}