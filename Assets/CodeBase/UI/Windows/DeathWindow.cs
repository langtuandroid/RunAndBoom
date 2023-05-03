using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;

        private Scene _scene;

        private void Start() =>
            _restartButton.onClick.AddListener(Restart);

        public void Construct(GameObject hero, Scene scene)
        {
            base.Construct(hero);
            _scene = scene;
        }

        // private void Restart() =>
        //     AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState>();


        private void Restart() =>
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, Scene>(_scene);
    }
}