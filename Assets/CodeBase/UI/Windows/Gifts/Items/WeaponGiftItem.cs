using CodeBase.Data.Progress;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class WeaponGiftItem : WeaponItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, HeroWeaponTypeId weaponTypeId, ProgressData progressData,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(weaponTypeId, progressData);
        }

        protected override void Clicked()
        {
            _progressData.WeaponsData.SetAvailableWeapon(_weaponTypeId);
            ClearData();
            _generator.Clicked();
            _audioService.LaunchShopSound(ShopSoundId.WeaponGotten, transform, _audioSource);
        }
    }
}