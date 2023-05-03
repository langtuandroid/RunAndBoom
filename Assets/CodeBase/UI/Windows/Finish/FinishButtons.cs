using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishButtons : MonoBehaviour
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private FinishWindow _finishWindow;
        [SerializeField] private ItemsGeneratorBase _generator;

        private IPlayerProgressService _playerProgressService;

        private void Awake()
        {
            _playerProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _skipButton.onClick.AddListener(CloseShop);
            _generator.GenerationStarted += DisableRefreshButtons;
            _generator.GenerationEnded += CheckRefreshButtons;
        }

        public void Construct()
        {
            GenerateItems();
        }

        private void DisableRefreshButtons()
        {
        }

        private void CheckRefreshButtons()
        {
        }

        private void Start() =>
            Cursor.lockState = CursorLockMode.Confined;

        private void CloseShop()
        {
            _finishWindow.Hide();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ShowAds()
        {
            //TODO ShowAds screen
        }

        private void GenerateItems() =>
            _generator.Generate();
    }
}