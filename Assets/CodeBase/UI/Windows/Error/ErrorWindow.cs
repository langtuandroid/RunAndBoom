using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Windows;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Error
{
    public class ErrorWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _errorText;

        [Inject]
        public void Construct(IPlayerProgressService progressService)
        {
            base.Construct(progressService);
        }

        protected override void Initialize() =>
            _errorText.text = CurrentError;

        public class Factory : PlaceholderFactory<IWindowService, ErrorWindow>
        {
        }
    }
}