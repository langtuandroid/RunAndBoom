using CodeBase.Data;
using CodeBase.Data.Settings;
using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner, IProgressReader
    {
        public LoadingCurtain CurtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private Language GetLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    return Language.RU;
                case SystemLanguage.Turkish:
                    return Language.TR;
                default:
                    return Language.EN;
            }
        }

        public void LoadProgress(PlayerProgress progress) =>
            progress.SettingsData.SetLanguage(GetLanguage());
    }
}