using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services;
using CodeBase.UI.Windows.Gifts;
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
        protected ILocalizationService LocalizationService;
        protected IInputService InputService;
        protected ProgressData ProgressData;
        protected ShopItemBalance ShopItemBalance;
        protected GiftsItemBalance GiftsItemBalance;
        protected float Volume;
        protected AudioSource AudioSource;
        private SettingsData _settingsData;

        private void Awake()
        {
            var parent = transform.parent;
            _shopItemHighlighter = parent.GetComponent<ShopItemHighlighter>();
            AudioSource = parent.GetComponent<AudioSource>();
            _settingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;
        }

        private void OnEnable()
        {
            _settingsData.SoundSwitchChanged += SwitchChanged;
            _settingsData.SoundVolumeChanged += VolumeChanged;
            VolumeChanged();
            SwitchChanged();
        }

        private void OnDisable()
        {
            _settingsData.SoundSwitchChanged -= SwitchChanged;
            _settingsData.SoundVolumeChanged -= VolumeChanged;
        }

        protected void Construct(ProgressData progressData)
        {
            ProgressData = progressData;
            StaticDataService = AllServices.Container.Single<IStaticDataService>();
            LocalizationService = AllServices.Container.Single<ILocalizationService>();
            InputService = AllServices.Container.Single<IInputService>();
            ShopItemBalance = new ShopItemBalance();
            GiftsItemBalance = new GiftsItemBalance();
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
            gameObject.SetActive(false);
        }

        public void ChangeClickability(bool isClickable) =>
            Button.enabled = isClickable;

        protected abstract void FillData();
        protected abstract void Clicked();

        private void VolumeChanged() =>
            Volume = _settingsData.SoundVolume;

        private void SwitchChanged() =>
            Volume = _settingsData.SoundOn ? _settingsData.SoundVolume : Constants.Zero;
    }
}