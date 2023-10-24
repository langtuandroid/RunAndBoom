using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Perks;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class PerkGiftItem : PerkItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, PerkItemData perkItemData, ProgressData progressData,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(perkItemData, progressData);
        }

        protected override void Clicked()
        {
            ProgressData.PerksData.LevelUp(_perkStaticData.PerkTypeId);
            ClearData();
            _generator.Clicked();

            switch (_perkStaticData.LevelTypeId)
            {
                case LevelTypeId.None:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_1),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
                case LevelTypeId.Level_1:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_2),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
                case LevelTypeId.Level_2:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.UpgradeLevel_3),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
            }
        }
    }
}