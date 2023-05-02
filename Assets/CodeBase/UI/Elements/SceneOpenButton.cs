using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public abstract class SceneOpenButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Scene _scene;

        private IGameStateMachine _stateMachine;
        protected IPlayerProgressService ProgressService;
        private IWindowService _windowService;
        private ISaveLoadService _saveLoadService;

        // protected abstract string Scene { get; }
        protected abstract bool Checked { get; }

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            ProgressService = AllServices.Container.Single<IPlayerProgressService>();
            _windowService = AllServices.Container.Single<IWindowService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _button.onClick.AddListener(Open);
        }

        private void Open()
        {
            _saveLoadService.SaveProgress();

            if (!Checked)
                _windowService.Open<ErrorWindow>(WindowId.Error);
            else
                _stateMachine.Enter<LoadSceneState, string>(_scene.ToString());
        }
    }
}