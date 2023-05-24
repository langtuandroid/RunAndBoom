using CodeBase.Data;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Gifts;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class MoneyGiftItem : MoneyItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(MoneyTypeId moneyTypeId, PlayerProgress progress, GiftsGenerator generator)
        {
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
                        transform: transform, Volume, AudioSource);
                    break;
                case MoneyTypeId.Banknote:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.FewMoneyGotten),
                        transform: transform, Volume, AudioSource);
                    break;
                case MoneyTypeId.Bag:
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.PouringMoneyGotten),
                        transform: transform, Volume, AudioSource);
                    break;
            }
        }
    }
}