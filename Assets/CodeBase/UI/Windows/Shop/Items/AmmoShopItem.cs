using CodeBase.Data;
using CodeBase.Data.Progress;
using CodeBase.Services.Audio;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Windows.Common;
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
            if (_shopItemBalance.IsMoneyEnough(_shopAmmoStaticData.Cost))
            {
                _shopItemBalance.ReduceMoney(_shopAmmoStaticData.Cost);
                _progressData.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId,
                    _inputService.GetCount(_shopAmmoStaticData.Count));
                ClearData();
                _audioService.LaunchShopSound(ShopSoundId.AmmoGotten, transform, _audioSource);
            }
            else
            {
                _audioService.LaunchShopSound(ShopSoundId.NotEnoughMoney, transform, _audioSource);
            }
        }
    }
}