using CodeBase.Data.Progress;
using CodeBase.Hero;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class ItemShopItem : ItemItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, ItemTypeId typeId, HeroHealth health, ProgressData progressData)
        {
            _heroTransform = heroTransform;
            base.Construct(typeId, health, progressData);
        }

        protected override void Clicked()
        {
            if (_shopItemBalance.IsMoneyEnough(_itemStaticData.Cost))
            {
                _shopItemBalance.ReduceMoney(_itemStaticData.Cost);

                if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                {
                    Health.Recover();
                    _audioService.LaunchShopSound(ShopSoundId.FullRecovery, transform, _audioSource);
                }

                ClearData();
            }
            else
            {
                _audioService.LaunchShopSound(ShopSoundId.NotEnoughMoney, transform, _audioSource);
            }
        }
    }
}