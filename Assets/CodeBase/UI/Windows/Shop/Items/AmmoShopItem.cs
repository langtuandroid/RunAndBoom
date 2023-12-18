using CodeBase.Data.Progress;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class AmmoShopItem : AmmoItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, AmmoItem ammoItem, ProgressData progressData)
        {
            _heroTransform = heroTransform;
            base.Construct(ammoItem, progressData);
        }

        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_shopAmmoStaticData.Cost);
                ProgressData.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId,
                    GetCount(_shopAmmoStaticData.Count));
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