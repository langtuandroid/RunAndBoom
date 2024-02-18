using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Upgrades;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class UpgradeShopItem : UpgradeItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, UpgradeItemData upgradeItemData, ProgressData progressData)
        {
            _heroTransform = heroTransform;
            base.Construct(upgradeItemData, progressData);
        }

        protected override void Clicked()
        {
            if (_shopItemBalance.IsMoneyEnough(_upgradeLevelInfoStaticData.Cost))
            {
                _shopItemBalance.ReduceMoney(_upgradeLevelInfoStaticData.Cost);
                _progressData.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                    _upgradeStaticData.UpgradeTypeId);

                switch (_upgradeLevelInfoStaticData.LevelTypeId)
                {
                    case LevelTypeId.Level_1:
                        _audioService.LaunchShopSound(ShopSoundId.UpgradeLevel_1, _heroTransform, _audioSource);
                        break;
                    case LevelTypeId.Level_2:
                        _audioService.LaunchShopSound(ShopSoundId.UpgradeLevel_2, _heroTransform, _audioSource);
                        break;
                    case LevelTypeId.Level_3:
                        _audioService.LaunchShopSound(ShopSoundId.UpgradeLevel_3, _heroTransform, _audioSource);
                        break;
                }

                ClearData();
            }
            else
            {
                _audioService.LaunchShopSound(ShopSoundId.NotEnoughMoney, _heroTransform, _audioSource);
            }
        }
    }
}