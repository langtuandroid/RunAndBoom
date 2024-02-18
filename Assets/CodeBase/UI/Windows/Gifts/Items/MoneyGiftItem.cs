using CodeBase.Data.Progress;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class MoneyGiftItem : MoneyItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, MoneyTypeId moneyTypeId, ProgressData progressData,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(moneyTypeId, progressData);
        }

        protected override void Clicked()
        {
            _giftsItemBalance.AddMoney(_moneyStaticData.Value);
            ClearData();
            _generator.Clicked();

            switch (_moneyStaticData.TypeId)
            {
                case MoneyTypeId.Coin:
                    _audioService.LaunchShopSound(ShopSoundId.OneMoneyGotten, _heroTransform, _audioSource);
                    break;
                case MoneyTypeId.Banknote:
                    _audioService.LaunchShopSound(ShopSoundId.FewMoneyGotten, _heroTransform, _audioSource);
                    break;
                case MoneyTypeId.Bag:
                    _audioService.LaunchShopSound(ShopSoundId.PouringMoneyGotten, _heroTransform, _audioSource);
                    break;
            }
        }
    }
}