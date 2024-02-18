using CodeBase.Data.Progress;
using CodeBase.Services;
using CodeBase.Services.Audio;
using CodeBase.Services.Input;
using CodeBase.Services.Localization;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services;
using CodeBase.UI.Windows.Gifts;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Common
{
    public abstract class ItemBase : MonoBehaviour
    {
        [FormerlySerializedAs("BackgroundIcon")] [SerializeField]
        protected Image _backgroundIcon;

        [FormerlySerializedAs("MainIcon")] [SerializeField]
        protected Image _mainIcon;

        [FormerlySerializedAs("LevelIcon")] [SerializeField]
        protected Image _levelIcon;

        [FormerlySerializedAs("AdditionalIcon")] [SerializeField]
        protected Image _additionalIcon;

        [FormerlySerializedAs("CostText")] [SerializeField]
        protected TextMeshProUGUI _costText;

        [FormerlySerializedAs("CountText")] [SerializeField]
        protected TextMeshProUGUI _countText;

        [FormerlySerializedAs("TitleText")] [SerializeField]
        protected TextMeshProUGUI _titleText;

        [FormerlySerializedAs("Button")] [SerializeField]
        protected Button _button;

        private ShopItemHighlighter _shopItemHighlighter;
        protected IStaticDataService _staticDataService;
        protected ILocalizationService _localizationService;
        protected IInputService _inputService;
        protected IAudioService _audioService;
        protected ProgressData _progressData;
        protected ShopItemBalance _shopItemBalance;
        protected GiftsItemBalance _giftsItemBalance;
        protected float _volume;
        protected AudioSource _audioSource;

        private void Awake()
        {
            var parent = transform.parent;
            _shopItemHighlighter = parent.GetComponent<ShopItemHighlighter>();
            _audioSource = parent.GetComponent<AudioSource>();
            _audioService = AllServices.Container.Single<IAudioService>();
        }

        protected void Construct(ProgressData progressData)
        {
            _progressData = progressData;
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _inputService = AllServices.Container.Single<IInputService>();
            _shopItemBalance = new ShopItemBalance();
            _giftsItemBalance = new GiftsItemBalance();
        }

        protected void ClearData()
        {
            if (_backgroundIcon != null)
                _backgroundIcon.ChangeImageAlpha(Constants.Invisible);

            if (_mainIcon != null)
                _mainIcon.ChangeImageAlpha(Constants.Invisible);

            if (_levelIcon != null)
                _levelIcon.ChangeImageAlpha(Constants.Invisible);

            if (_additionalIcon != null)
                _additionalIcon.ChangeImageAlpha(Constants.Invisible);

            if (_costText != null)
                _costText.text = "";

            if (_countText != null)
                _countText.text = "";

            if (_titleText != null)
                _titleText.text = "";

            _shopItemHighlighter.enabled = false;
            gameObject.SetActive(false);
        }

        public void ChangeClickability(bool isClickable) =>
            _button.enabled = isClickable;

        protected abstract void FillData();
        protected abstract void Clicked();
    }
}