using CodeBase.Data;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class AmmoShopItem : AmmoItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, AmmoItem ammoItem, PlayerProgress progress)
        {
            _heroTransform = heroTransform;
            base.Construct(ammoItem, progress);
        }


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
                    transform: _heroTransform, Volume, AudioSource);
            }
            else
            {
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.NotEnoughMoney),
                    transform: _heroTransform, Volume, AudioSource);
            }
        }
    }
}