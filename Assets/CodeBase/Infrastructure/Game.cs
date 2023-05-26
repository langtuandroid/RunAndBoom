using CodeBase.Data.Settings;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine =
                new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container,
                    GetLanguage());
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
    }
}