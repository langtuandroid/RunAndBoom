using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;

        private string _sceneName;

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
            _closeButton.onClick.AddListener(Hide);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Hide();
        }

        public void Construct(GameObject hero, string sceneName)
        {
            _sceneName = sceneName;
            base.Construct(hero);
        }

        private void Restart()
        {
            Hide();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, string>(_sceneName);
        }
    }
}