using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _activeBackground;

        private bool _mouseOver;
        private bool _isVisible = true;

        private void Update()
        {
            // if (_isVisible)
            _activeBackground.ChangeImageAlpha(_mouseOver ? Constants.AlphaActiveItem : Constants.AlphaInactiveItem);
            // else
            //     _activeBackground.ChangeImageAlpha(Constants.AlphaInactiveItem);
        }

        private void OnEnable()
        {
            // SetVisibility(true);
        }

        private void OnDisable()
        {
            // SetVisibility(false);
            _activeBackground.ChangeImageAlpha(Constants.AlphaInactiveItem);
        }

        private void SetVisibility(bool isVisible)
        {
            _isVisible = isVisible;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseOver = false;
        }
    }
}