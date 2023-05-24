using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.Audio;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;

        private IPlayerProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private Scene _scene;

        private void Awake()
        {
            _progressService = AllServices.Container.Single<IPlayerProgressService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void Start() =>
            _restartButton.onClick.AddListener(Restart);

        public void Construct(GameObject hero, Scene scene)
        {
            base.Construct(hero, WindowId.Death);
            _scene = scene;
        }

        private void Restart()
        {
            WindowService.HideAll();
            SoundInstance.StopRandomMusic();
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadPlayerProgressState, Scene>(_scene);
        }

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.Death), transform: transform,
                Volume, AudioSource);
        }
    }
}