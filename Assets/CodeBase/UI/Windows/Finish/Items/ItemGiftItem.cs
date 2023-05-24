using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class ItemGiftItem : ItemItemBase
    {
        private GiftsGenerator _generator;

        // public void Construct(ItemTypeId typeId, PlayerProgress progress, GameObject hero, GiftsGenerator generator)
        public void Construct(ItemTypeId typeId, PlayerProgress progress, HeroHealth health, GiftsGenerator generator)
        {
            _generator = generator;
            // base.Construct(typeId, progress, hero);
            base.Construct(typeId, progress, health);
        }

        protected override void Clicked()
        {
            if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
            {
                Health.Recover();
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.FullRecovery),
                    transform: transform, Volume, AudioSource);
            }

            ClearData();
            _generator.Clicked();
        }
    }
}