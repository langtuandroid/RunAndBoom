using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services;
using CodeBase.UI.Windows.FinishLevel;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Common
{
    public abstract class ItemBase : MonoBehaviour
    {
        [SerializeField] protected Image BackgroundIcon;
        [SerializeField] protected Image MainIcon;
        [SerializeField] protected Image LevelIcon;
        [SerializeField] protected Image AdditionalIcon;
        [SerializeField] protected TextMeshProUGUI CostText;
        [SerializeField] protected TextMeshProUGUI CountText;
        [SerializeField] protected TextMeshProUGUI TitleText;
        [SerializeField] protected Button Button;

        private ShopItemHighlighter _shopItemHighlighter;
        protected IStaticDataService StaticDataService;
        protected PlayerProgress Progress;
        protected ShopItemBalance ShopItemBalance;
        protected GiftsItemBalance GiftsItemBalance;

        private void Awake() =>
            _shopItemHighlighter = transform.parent.GetComponent<ShopItemHighlighter>();

        protected void Construct(PlayerProgress progress)
        {
            Progress = progress;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            ShopItemBalance = new ShopItemBalance();
            GiftsItemBalance = new GiftsItemBalance();
        }

        protected void ClearData()
        {
            if (BackgroundIcon != null)
                BackgroundIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (MainIcon != null)
                MainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (LevelIcon != null)
                LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (AdditionalIcon != null)
                AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);

            if (CostText != null)
                CostText.text = "";

            if (CountText != null)
                CountText.text = "";

            if (TitleText != null)
                TitleText.text = "";

            _shopItemHighlighter.enabled = false;
            // _shopItemHighlighter.SetVisibility(false);
            gameObject.SetActive(false);
        }

        public void ChangeClickability(bool isClickable) =>
            Button.enabled = isClickable;

        protected abstract void FillData();
        protected abstract void Clicked();
    }
}