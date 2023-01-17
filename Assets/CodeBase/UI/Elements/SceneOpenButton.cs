using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public abstract class SceneOpenButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IGameStateMachine _stateMachine;
        protected IPlayerProgressService ProgressService;
        private IWindowService _windowService;
        private ISaveLoadService _saveLoadService;

        protected void Construct(IGameStateMachine stateMachine, IPlayerProgressService progressService, IWindowService windowService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            ProgressService = progressService;
            _windowService = windowService;
            _saveLoadService = saveLoadService;
        }

        protected abstract string Scene { get; }
        protected abstract bool Checked { get; }

        private void Awake()
        {
            _button.onClick.AddListener(Open);
        }

        private void Open()
        {
            _saveLoadService.SaveProgress();

            if (!Checked)
                _windowService.Open(WindowId.Error);
            else
                _stateMachine.Enter<LoadSceneState, string>(Scene);
        }
    }
}