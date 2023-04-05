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
        public Image MainIcon;
        public Image LevelIcon;
        public Image AdditionalIcon;
        public TextMeshProUGUI CostText;
        public TextMeshProUGUI CountText;
        public TextMeshProUGUI TitleText;
        public Button Button;

        protected IStaticDataService StaticDataService;
        protected IPlayerProgressService PlayerProgressService;
        protected PlayerProgress Progress;

        protected void Construct(IPlayerProgressService playerProgressService)
        {
            PlayerProgressService = playerProgressService;
            Button.onClick.AddListener(Clicked);
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
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