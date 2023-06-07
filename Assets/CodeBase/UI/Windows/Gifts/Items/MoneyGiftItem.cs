using CodeBase.Data;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class MoneyGiftItem : MoneyItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, MoneyTypeId moneyTypeId, PlayerProgress progress,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(moneyTypeId, progress);
        }

        protected override void Clicked()
        {
            GiftsItemBalance.AddMoney(_moneyStaticData.Value);
            ClearData();
            _generator.Clicked();

            switch (_moneyStaticData.TypeId)
            {
                case MoneyTypeId.Coin:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.OneMoneyGotten),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
                case MoneyTypeId.Banknote:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.FewMoneyGotten),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
                case MoneyTypeId.Bag:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.PouringMoneyGotten),
                        transform: _heroTransform, Volume, AudioSource);
                    break;
            }
        }
    }
}