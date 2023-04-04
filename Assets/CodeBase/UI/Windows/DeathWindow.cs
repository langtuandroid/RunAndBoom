using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;

        private string _sceneName;

        private void Start() =>
            _restartButton.onClick.AddListener(Restart);

        public void Construct(GameObject hero, string sceneName)
        {
            _sceneName = sceneName;
            base.Construct(hero);
        }

        private void Restart() =>
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, string>(_sceneName);
    }
}