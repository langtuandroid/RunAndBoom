using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop.ViewItems
{
    public abstract class BaseItemView : MonoBehaviour
    {
        public Image BackgroundIcon;
        public Image MainIcon;
        public Image LevelIcon;
        public Image AdditionalIcon;
        public TextMeshProUGUI CostText;
        public TextMeshProUGUI CountText;

        public TextMeshProUGUI TitleText;
        // protected Button Button;

        private ShopItemHighlighter _shopItemHighlighter;

        protected IStaticDataService StaticDataService;

        protected PlayerProgress Progress;

        private void Awake() =>
            _shopItemHighlighter = transform.parent.GetComponent<ShopItemHighlighter>();

        protected void Construct(PlayerProgress progress)
        {
            Progress = progress;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
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

        protected bool IsMoneyEnough(int value) =>
            Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        protected void ReduceMoney(int value) =>
            Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        protected abstract void FillData();
    }
}