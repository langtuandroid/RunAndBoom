using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _outline;

        private void OnDisable() =>
            _outline.SetActive(false);

        public void OnPointerEnter(PointerEventData eventData) =>
            _outline.SetActive(true);

        public void OnPointerExit(PointerEventData eventData) =>
            _outline.SetActive(false);
    }
}