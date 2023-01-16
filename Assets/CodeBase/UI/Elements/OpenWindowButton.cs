using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowId _windowId;

        [Inject] private IWindowService _windowService;

        private void Awake()
        {
            _button.onClick.AddListener(Open);
        }

        private void Open() =>
            _windowService.Open(_windowId);
    }
}