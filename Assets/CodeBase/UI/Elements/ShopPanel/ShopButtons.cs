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

        private void Awake()
        {
            _skipButton.onClick.AddListener(CloseShop);
            _refreshButton.onClick.AddListener(CloseShop);
            _refresh = _refreshButton.GetComponent<RefreshButton>();
            // _refreshButton.onClick.AddListener(() => _generator.GenerateItems());
        }

        public void ShowRefreshButton() =>
            _refreshButton.gameObject.SetActive(true);

        public void HideRefreshButton() =>
            _refreshButton.gameObject.SetActive(false);

        private void CloseShop() =>
            _shopWindow.Hide();
    }
}