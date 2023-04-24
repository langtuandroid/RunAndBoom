using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class ShopButtons : MonoBehaviour
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private ShopItemsGenerator _generator;

        private RefreshButton _refresh;
        private int _currentRefreshCount = 0;
        private int _maxRefreshCount;
        private int _watchAdsNumber;

        private void Awake()
        {
            _skipButton.onClick.AddListener(CloseShop);
            _refreshButton.onClick.AddListener(CloseShop);
            _refresh = _refreshButton.GetComponent<RefreshButton>();
            _refreshButton.onClick.AddListener(GenerateShopItems);
            _generator.GenerationStarted += DisableRefreshButtonClick;
            _generator.GenerationEnded += EnableRefreshButtonClick;
        }

        public void Construct(int maxRefreshCount, int watchAdsNumber)
        {
            _maxRefreshCount = maxRefreshCount;
            _watchAdsNumber = watchAdsNumber;
            CheckCurrentEqualsWatchAdsNumber();
            CheckCurrentEqualsMaxCount();
        }

        private void DisableRefreshButtonClick() =>
            _refreshButton.enabled = false;

        private void EnableRefreshButtonClick()
        {
            // if (_watchAdsNumber == _currentRefreshCount)
            //TODO ShowAds screen

            _currentRefreshCount++;
            CheckCurrentEqualsWatchAdsNumber();
            CheckCurrentEqualsMaxCount();
            _refreshButton.enabled = true;
        }

        private void GenerateShopItems() =>
            _generator.GenerateShopItems();

        private void CheckCurrentEqualsWatchAdsNumber()
        {
            if (_watchAdsNumber == _currentRefreshCount)
                EnableWatchAdsIcon();
            else
                DisableWatchAdsIcon();
        }

        private void CheckCurrentEqualsMaxCount()
        {
            if (_maxRefreshCount > _currentRefreshCount)
                ShowRefreshButton();
            else
                HideRefreshButton();
        }

        private void DisableWatchAdsIcon() =>
            _refresh.HideAdsIcon();

        private void EnableWatchAdsIcon() =>
            _refresh.ShowAdsIcon();

        private void ShowRefreshButton() =>
            _refreshButton.gameObject.SetActive(true);

        private void HideRefreshButton() =>
            _refreshButton.gameObject.SetActive(false);

        private void Start() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void CloseShop()
        {
            _shopWindow.Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}