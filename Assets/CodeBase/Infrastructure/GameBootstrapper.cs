using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private AudioBackgroundChanger _audioBackgroundChangerPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            Instantiate(_audioBackgroundChangerPrefab);

            DontDestroyOnLoad(this);
        }
    }
}