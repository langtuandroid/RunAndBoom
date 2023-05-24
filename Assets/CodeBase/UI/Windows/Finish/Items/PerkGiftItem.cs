using CodeBase.Data;
using CodeBase.Data.Perks;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class PerkGiftItem : PerkItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(PerkItemData perkItemData, PlayerProgress progress, GiftsGenerator generator)
        {
            _generator = generator;
            base.Construct(perkItemData, progress);
        }

        protected override void Clicked()
        {
            Progress.PerksData.LevelUp(_perkStaticData.PerkTypeId);
            ClearData();
            _generator.Clicked();

            switch (_perkStaticData.LevelTypeId)
            {
                case LevelTypeId.None:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_1),
                        transform: transform, Volume, AudioSource);
                    break;
                case LevelTypeId.Level_1:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_2),
                        transform: transform, Volume, AudioSource);
                    break;
                case LevelTypeId.Level_2:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_3),
                        transform: transform, Volume, AudioSource);
                    break;
            }
        }
    }
}