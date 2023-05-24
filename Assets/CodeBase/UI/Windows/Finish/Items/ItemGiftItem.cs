using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class ItemGiftItem : ItemItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, ItemTypeId typeId, PlayerProgress progress, HeroHealth health,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(typeId, health, progress);
        }

        protected override void Clicked()
        {
            if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
            {
                Health.Recover();
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.FullRecovery),
                    transform: _heroTransform, Volume, AudioSource);
            }

            ClearData();
            _generator.Clicked();
        }
    }
}