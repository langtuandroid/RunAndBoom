using UnityEngine;

namespace CodeBase.UI.Elements.Hud.ShopPanel
{
    public class ShopRender : MonoBehaviour
    {
        [SerializeField] private GameObject _shop;

        private void Awake() =>
            Hide();

        public void Show() =>
            _shop.SetActive(true);

        public void Hide() =>
            _shop.SetActive(false);
    }
}