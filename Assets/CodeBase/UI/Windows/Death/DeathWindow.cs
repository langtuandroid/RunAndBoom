using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;

        private void Start() =>
            _restartButton.onClick.AddListener(Restart);

        public void Construct(GameObject hero) =>
            base.Construct(hero, WindowId.Death);

        private void Restart()
        {
            WindowService.HideAll();
            SoundInstance.StopRandomMusic();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState>();
        }

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.Death), transform: transform,
                Volume, AudioSource);
        }
    }
}