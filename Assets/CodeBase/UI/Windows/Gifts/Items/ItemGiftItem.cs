using CodeBase.Data.Progress;
using CodeBase.Hero;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class ItemGiftItem : ItemItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, ItemTypeId typeId, ProgressData progressData, HeroHealth health,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(typeId, health, progressData);
        }

        protected override void Clicked()
        {
            if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
            {
                Health.Recover();
                _audioService.LaunchShopSound(ShopSoundId.FullRecovery, transform, _audioSource);
            }

            ClearData();
            _generator.Clicked();
        }
    }
}