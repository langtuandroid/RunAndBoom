using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Perks;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
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
            _progressData.PerksData.LevelUp(_perkStaticData.PerkTypeId);
            ClearData();
            _generator.Clicked();

            switch (_perkStaticData.LevelTypeId)
            {
                case LevelTypeId.None:
                    _audioService.LaunchShopSound(ShopSoundId.UpgradeLevel_1, _heroTransform, _audioSource);
                    break;
                case LevelTypeId.Level_1:
                    _audioService.LaunchShopSound(ShopSoundId.UpgradeLevel_2, _heroTransform, _audioSource);
                    break;
                case LevelTypeId.Level_2:
                    _audioService.LaunchShopSound(ShopSoundId.UpgradeLevel_3, _heroTransform, _audioSource);
                    break;
            }
        }
    }
}