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

        private void Awake()
        {
            _skipButton.onClick.AddListener(CloseShop);
            _refreshButton.onClick.AddListener(CloseShop);
            // _refreshButton.onClick.AddListener(() => _generator.GenerateItems());
        }

        private void CloseShop()
        {
            _shopWindow.Hide();
            Time.timeScale = 1;
        }
    }
}