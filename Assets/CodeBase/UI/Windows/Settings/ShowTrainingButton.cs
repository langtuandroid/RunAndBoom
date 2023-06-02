using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class ShowTrainingButton : MonoBehaviour, IProgressReader
    {
        [SerializeField] private Button _showTrainingButton;
        [SerializeField] protected Image ImageSelected;
        [SerializeField] protected Image ImageUnselected;

        private PlayerProgress _progress;

        private void Awake() =>
            _showTrainingButton.onClick.AddListener(Clicked);

        private void Clicked() =>
            _progress.SettingsData.ChangeTrainingSwitch();

        public void LoadProgress(PlayerProgress progress)
        {
            _progress = progress;
            _progress.SettingsData.ShowTrainingSwitchChanged += ShowTrainingSwitchChanged;
            ShowTrainingSwitchChanged();
        }

        private void ShowTrainingSwitchChanged()
        {
            if (_progress.SettingsData.ShowTraining)
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
    }
}