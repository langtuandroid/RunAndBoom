using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Windows.Common;
using UnityEngine;

namespace CodeBase.UI.Windows.Gifts.Items
{
    public class AmmoGiftItem : AmmoItemBase
    {
        private GiftsGenerator _generator;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform, AmmoItem ammoItem, ProgressData progressData,
            GiftsGenerator generator)
        {
            _heroTransform = heroTransform;
            _generator = generator;
            base.Construct(ammoItem, progressData);
        }

        protected override void Clicked()
        {
            _progressData.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId,
                _inputService.GetCount(_shopAmmoStaticData.Count));
            ClearData();
            _generator.Clicked();
            _audioService.LaunchShopSound(ShopSoundId.AmmoGotten, transform, _audioSource);
        }
    }
}