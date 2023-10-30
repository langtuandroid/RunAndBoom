using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings.AimSensitive
{
    public class AimHorizontalSensitiveSlider : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Slider _slider;

        private SettingsData _settingsData;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _slider.minValue = Constants.MinAimSliderValue;
            _slider.maxValue = Constants.MaxAimSliderValue;
        }

        private void Start()
        {
            if (_settingsData == null)
                _settingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;

            if (_saveLoadService == null)
                _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnEnable() =>
            _slider.onValueChanged.AddListener(ChangeValue);

        private void OnDisable() =>
            _slider.onValueChanged.RemoveListener(ChangeValue);

        private void ChangeValue(float value)
        {
            _settingsData.SetAimHorizontalSensitiveMultiplier(value);
            _saveLoadService.SaveHorizontalAimValue(value);
        }

        public void LoadProgressData(ProgressData progressData)
        {
            _settingsData.SetAimHorizontalSensitiveMultiplier(_settingsData.AimHorizontalSensitiveMultiplier);
            ChangeSliderValue(_settingsData.AimHorizontalSensitiveMultiplier);
        }

        private void ChangeSliderValue(float value) =>
            _slider.value = value;
    }
}