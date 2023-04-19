using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements.ShopPanel.ViewItems
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

        protected IStaticDataService StaticDataService;
        protected IPlayerProgressService PlayerProgressService;
        protected PlayerProgress Progress;

        private void Awake() =>
            BackgroundIcon = GetComponent<Image>();

        protected void Construct(IPlayerProgressService playerProgressService)
        {
            PlayerProgressService = playerProgressService;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
        }

        public void ClearData()
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

            gameObject.SetActive(false);
        }

        // public void ChangeClickability(bool isClickable) =>
        //     Button.interactable = isClickable;

        protected bool IsMoneyEnough(int value) =>
            Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        protected void ReduceMoney(int value) =>
            Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        protected abstract void FillData();
    }
}