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
    public abstract class BaseShopView : MonoBehaviour, IProgressReader
    {
        public Image MainIcon;
        public Image LevelIcon;
        public Image AdditionalIcon;
        public TextMeshProUGUI CostText;
        public TextMeshProUGUI CountText;
        public TextMeshProUGUI TitleText;
        public Button Button;

        protected IStaticDataService StaticDataService;
        protected PlayerProgress Progress;

        private void Awake() =>
            StaticDataService = AllServices.Container.Single<IStaticDataService>();

        protected void Construct()
        {
            Button.onClick.AddListener(Clicked);
        }

        public void LoadProgress(PlayerProgress progress) =>
            Progress = progress;

        protected void ClearData()
        {
            MainIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            LevelIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            AdditionalIcon.ChangeImageAlpha(Constants.AlphaInactiveItem);
            CostText.text = "";
            CountText.text = "";
            TitleText.text = "";
        }

        protected bool IsMoneyEnough(int value) =>
            Progress.CurrentLevelStats.MoneyData.IsMoneyEnough(value);

        protected void ReduceMoney(int value) =>
            Progress.CurrentLevelStats.MoneyData.ReduceMoney(value);

        protected abstract void FillData();
        protected abstract void Clicked();
    }
}