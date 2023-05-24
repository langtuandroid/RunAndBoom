using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;

        private ISaveLoadService _saveLoadService;
        private Scene _scene;

        private void Awake() =>
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

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

        public void Construct(GameObject hero, Scene scene)
        {
            _scene = scene;
            base.Construct(hero, WindowId.Settings);
        }

        private void Restart()
        {
            Hide();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, Scene>(_scene);
        }
    }
}