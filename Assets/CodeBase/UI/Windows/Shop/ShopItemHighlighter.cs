using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _outline;

        // private bool _mouseOver;

        private void Update()
        {
            // _outline.SetActive(_mouseOver);
        }

        private void OnDisable() => 
            _outline.SetActive(false);

        public void OnPointerEnter(PointerEventData eventData)
        {
            // _mouseOver = true;
            _outline.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // _mouseOver = false;
            _outline.SetActive(false);
        }
    }
}