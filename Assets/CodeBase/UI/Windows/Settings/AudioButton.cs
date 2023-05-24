using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public abstract class AudioButton : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Button _button;
        [SerializeField] protected Image ImageSelected;
        [SerializeField] protected Image ImageUnselected;

        protected PlayerProgress Progress;
        protected bool IsSelected;

        private void Awake() =>
            _button.onClick.AddListener(ButtonPressed);

        private void ButtonPressed()
        {
            SwitchAudio();
            ChangeImage();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Progress = progress;
            SetSelection();
            ChangeImage();
        }

        private void ChangeImage()
        {
            if (IsSelected)
            {
                ImageSelected.ChangeImageAlpha(Constants.AlphaActiveItem);
                ImageUnselected.ChangeImageAlpha(Constants.AlphaInactiveItem);
            }
            else
            {
                ImageUnselected.ChangeImageAlpha(Constants.AlphaActiveItem);
                ImageSelected.ChangeImageAlpha(Constants.AlphaInactiveItem);
            }
        }

        protected abstract void SwitchAudio();
        protected abstract void SetSelection();
    }
}