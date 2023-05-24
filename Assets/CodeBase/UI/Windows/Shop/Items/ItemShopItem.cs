using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class ItemShopItem : ItemItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_itemStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_itemStaticData.Cost);

                if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                {
                    Health.Recover();
                    SoundInstance.InstantiateOnTransform(
                        audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.FullRecovery),
                        transform: transform, Volume, AudioSource);
                }

                ClearData();
            }
            else
            {
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.NotEnoughMoney),
                    transform: transform, Volume, AudioSource);
            }
        }
    }
}