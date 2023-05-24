using CodeBase.Data;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
using Plugins.SoundInstance.Core.Static;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop.Items
{
    public class WeaponShopItem : WeaponItemBase
    {
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, HeroWeaponTypeId weaponTypeId, PlayerProgress progress)
        {
            _heroTransform = heroTransform;
            base.Construct(weaponTypeId, progress);
        }

        protected override void Clicked()
        {
            if (ShopItemBalance.IsMoneyEnough(_weaponStaticData.Cost))
            {
                ShopItemBalance.ReduceMoney(_weaponStaticData.Cost);
                Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
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