using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Finish
{
    public class FinishLevelButtons : MonoBehaviour
    {
        [SerializeField] private Button _skipButton;

        [FormerlySerializedAs("_shopWindow")] [SerializeField]
        private FinishWindow finishWindow;

        [SerializeField] private ItemsGeneratorBase _generator;

        private int _currentRefreshCount = 0;
        private int _maxRefreshCount;
        private int _watchAdsNumber;

        private void Awake()
        {
            _skipButton.onClick.AddListener(CloseShop);
            _generator.GenerationStarted += DisableRefreshButtons;
            _generator.GenerationEnded += CheckRefreshButtons;
        }

        public void Construct(int maxRefreshCount, int watchAdsNumber)
        {
            _maxRefreshCount = maxRefreshCount;
            _watchAdsNumber = watchAdsNumber;
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
            finishWindow.Hide();
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