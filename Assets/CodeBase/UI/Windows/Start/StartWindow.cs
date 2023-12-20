using CodeBase.UI.Elements.Hud;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Start
{
    public class StartWindow : WindowBase
    {
        [SerializeField] private Button _startButton;

        private void OnEnable() =>
            _startButton.onClick.AddListener(Close);

        private void OnDisable() =>
            _startButton.onClick.RemoveListener(Close);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("StartWindow Close");
                Close();
            }
        }

        public void Construct(GameObject hero, OpenSettings openSettings) =>
            base.Construct(hero, WindowId.Start, openSettings);

        private void Close() =>
            Hide();
    }
}