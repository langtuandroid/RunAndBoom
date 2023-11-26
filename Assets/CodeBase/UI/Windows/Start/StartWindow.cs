using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Start
{
    public class StartWindow : WindowBase
    {
        [SerializeField] private Button _startButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(Close);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
        }

        private void Close() =>
            Hide();

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Start);
    }
}