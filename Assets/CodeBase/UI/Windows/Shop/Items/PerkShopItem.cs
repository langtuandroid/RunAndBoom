using CodeBase.Data.Progress;
using CodeBase.Data.Progress.Perks;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class PerkShopItem : PerkItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, PerkItemData perkItemData, ProgressData progressData)
        {
            _heroTransform = heroTransform;
            base.Construct(perkItemData, progressData);
        }

        protected override void Clicked()
        {
            if (_shopItemBalance.IsMoneyEnough(_perkStaticData.Cost))
            {
                _shopItemBalance.ReduceMoney(_perkStaticData.Cost);
                _progressData.PerksData.LevelUp(_perkStaticData.PerkTypeId);
                ClearData();

                switch (_perkStaticData.ILevelTypeId)
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
            }
            else
            {
                _audioService.LaunchShopSound(ShopSoundId.NotEnoughMoney, _heroTransform, _audioSource);
            }
        }
    }
}