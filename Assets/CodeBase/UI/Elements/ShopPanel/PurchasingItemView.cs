using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel
{
    public class PurchasingItemView : MonoBehaviour
    {
        [SerializeField] private Image _mainIcon;
        [SerializeField] private Image _levelIcon;
        [SerializeField] private Image _additionalIcon;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Button _button;

        private IStaticDataService _staticDataService;
        private IPlayerProgressService _progressService;

        protected void Construct(IPlayerProgressService playerProgressService)
        {
            _progressService = playerProgressService;
            _button.onClick.AddListener(Clicked);
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
        }

        public void ClearData()
        {
            _mainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _levelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _additionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _costText.text = "";
            _countText.text = "";
            _titleText.text = "";
        }

        public void FillData(Sprite mainIcon, [CanBeNull] Sprite levelIcon, [CanBeNull] Sprite additionalIcon, int cost, string count,
            string title)
        {
            _mainIcon.sprite = mainIcon;
            _levelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _additionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            _costText.text = $"{cost} $";
            _costText.color = Constants.ShopItemPerk;
            _countText.text = count;
            _titleText.text = title;
        }

        private void Clicked()
        {
        }
    }
}