using CodeBase.Data.Progress;
using CodeBase.Data.Settings;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public abstract class AudioSlider : MonoBehaviour, IProgressReader
    {
        [SerializeField] protected Slider Slider;

        protected SettingsData SettingsData;
        protected float Volume;
        protected float PreviousVolume;
        protected bool IsSwitched;

        private void Awake() =>
            Slider.onValueChanged.AddListener(ChangeValue);

        private void Start()
        {
            if (SettingsData == null)
                SettingsData = AllServices.Container.Single<IPlayerProgressService>().SettingsData;
        }

        public void LoadProgressData(ProgressData progressData)
        {
            Subscribe();
            SwitchChanged();
            VolumeChanged();
        }

        protected abstract void ChangeValue(float value);

        protected abstract void Subscribe();

        protected abstract void Unsubscribe();

        protected abstract void SwitchChanged();

        protected abstract void ChangeVolume(float value);

        protected abstract void VolumeChanged();
    }
}