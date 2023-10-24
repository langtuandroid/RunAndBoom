using CodeBase.Data.Progress;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class WeaponShopItem : WeaponItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, HeroWeaponTypeId weaponTypeId, ProgressData progressData)
        {
            _heroTransform = heroTransform;
            base.Construct(weaponTypeId, progressData);
        }

        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_weaponStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_weaponStaticData.Cost);
                ProgressData.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ClearData();
                SoundInstance.InstantiateOnTransform(
                    audioClip: SoundInstance.GetClipFromLibrary(AudioClipAddresses.WeaponGotten),
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