using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using CodeBase.UI.Services;
using CodeBase.UI.Windows.Gifts;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Common
{
    public abstract class ItemBase : MonoBehaviour, IProgressReader
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
        protected ILocalizationService LocalizationService;
        protected PlayerProgress Progress;
        protected ShopItemBalance ShopItemBalance;
        protected GiftsItemBalance GiftsItemBalance;
        private PlayerProgress _progress;
        private const float BaseRatio = 1.0f;
        protected float MaxHealthRatio = 1.0f;
        protected float Volume;
        protected AudioSource AudioSource;

        private void Awake()
        {
            var parent = transform.parent;
            _shopItemHighlighter = parent.GetComponent<ShopItemHighlighter>();
            AudioSource = parent.GetComponent<AudioSource>();
        }

        protected void Construct(PlayerProgress progress)
        {
            Progress = progress;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            LocalizationService = AllServices.Container.Single<ILocalizationService>();
            ShopItemBalance = new ShopItemBalance();
            GiftsItemBalance = new GiftsItemBalance();

            SetMaxHealth();
        }

        private void SetMaxHealth()
        {
            var upMaxHealthItemData = _progress.PerksData.Perks.Find(x => x.PerkTypeId == PerkTypeId.UpMaxHealth);

            if (upMaxHealthItemData.LevelTypeId == LevelTypeId.None)
                MaxHealthRatio = BaseRatio;
            else
                MaxHealthRatio =
                    StaticDataService.ForPerk(PerkTypeId.UpMaxHealth, upMaxHealthItemData.LevelTypeId).Value;
        }

        protected void ClearData()
        {
            if (BackgroundIcon != null)
                BackgroundIcon.ChangeImageAlpha(Constants.Invisible);

            if (MainIcon != null)
                MainIcon.ChangeImageAlpha(Constants.Invisible);

            if (LevelIcon != null)
                LevelIcon.ChangeImageAlpha(Constants.Invisible);

            if (AdditionalIcon != null)
                AdditionalIcon.ChangeImageAlpha(Constants.Invisible);

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

        public void ChangeClickability(bool isClickable)
        {
            Button.enabled = isClickable;
        }

        protected abstract void FillData();
        protected abstract void Clicked();

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.SettingsData.SoundSwitchChanged += SwitchChanged;
            _progress.SettingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void VolumeChanged()
        {
            Volume = _progress.SettingsData.SoundVolume;
        }

        private void SwitchChanged()
        {
            Volume = _progress.SettingsData.SoundOn ? _progress.SettingsData.SoundVolume : Constants.Zero;
        }
    }
}