using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Upgrades;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class UpgradeGiftItem : UpgradeItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, UpgradeItemData upgradeItemData, ProgressData progressData,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(upgradeItemData, progressData);
        }

        protected override void Clicked()
        {
            _progressData.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                _upgradeStaticData.UpgradeTypeId);
            ClearData();
            _generator.Clicked();

            switch (_upgradeLevelInfoStaticData.LevelTypeId)
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