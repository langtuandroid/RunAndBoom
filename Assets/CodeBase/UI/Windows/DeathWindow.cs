using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;

        private void Start() =>
            _restartButton.onClick.AddListener(Restart);

        private void Restart()
        {
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState>();
        }
    }
}