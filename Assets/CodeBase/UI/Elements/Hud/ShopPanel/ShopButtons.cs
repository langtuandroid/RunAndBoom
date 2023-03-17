using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.Hud.ShopPanel
{
    public class ShopButtons : MonoBehaviour
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private GameObject _shop;
        [SerializeField] private ShopItemsGenerator _generator;

        private void Awake()
        {
            _skipButton.onClick.AddListener(CloseShop);
            _refreshButton.onClick.AddListener(CloseShop);
            // _refreshButton.onClick.AddListener(() => _generator.GenerateItems());
        }

        private void CloseShop()
        {
            _shop.SetActive(false);
            Time.timeScale = 1;
        }
    }
}