using CodeBase.Hero;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Death
{
    public class DeathWindow : WindowBase
    {
        [SerializeField] private Button _recoverForAdsButton;
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            _recoverForAdsButton.onClick.AddListener(RecoverForAds);
            _restartButton.onClick.AddListener(Restart);
        }

        private void RecoverForAds()
        {
            //TODO show ads

            RecoverHealth();
            Hide();
        }

        private void RecoverHealth() =>
            Hero.GetComponent<HeroHealth>().Recover();

        public void Construct(GameObject hero)
        {
            base.Construct(hero, WindowId.Death);
        }

        protected override void PlayOpenSound()
        {
            SoundInstance.InstantiateOnTransform(
                audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.Death), transform: transform,
                Volume, AudioSource);
        }
    }
}