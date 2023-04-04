using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class RefreshButton : MonoBehaviour
    {
        [SerializeField] private Image _adsIcon;

        public void ShowAdsIcon() =>
            _adsIcon.ChangeImageAlpha(Constants.AlphaActiveItem);

        public void HideAdsIcon() =>
            _adsIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
    }
}