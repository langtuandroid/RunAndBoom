using CodeBase.Data.Progress;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
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
            if (_shopItemBalance.IsMoneyEnough(_weaponStaticData.Cost))
            {
                _shopItemBalance.ReduceMoney(_weaponStaticData.Cost);
                _progressData.WeaponsData.SetAvailableWeapon(_weaponTypeId);
                ClearData();
                _audioService.LaunchShopSound(ShopSoundId.WeaponGotten, _heroTransform, _audioSource);
            }
            else
            {
                _audioService.LaunchShopSound(ShopSoundId.NotEnoughMoney, _heroTransform, _audioSource);
            }
        }
    }
}