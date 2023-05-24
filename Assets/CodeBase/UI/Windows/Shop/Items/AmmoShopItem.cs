using CodeBase.Services.Audio;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class AmmoShopItem : AmmoItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_shopAmmoStaticData.Cost);
                int value = _shopAmmoStaticData.Count.GetHashCode();
                Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, value);
                ClearData();
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.AmmoGotten),
                    transform: transform, Volume, AudioSource);
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