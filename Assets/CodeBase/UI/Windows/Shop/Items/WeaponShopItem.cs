using CodeBase.Services.Audio;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class WeaponShopItem : WeaponItemBase
    {
        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_weaponStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_weaponStaticData.Cost);
                Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ClearData();
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.WeaponGotten),
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