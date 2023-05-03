using CodeBase.Data;
using CodeBase.StaticData.Items.Shop.Ammo;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class AmmoGiftItem : AmmoItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(AmmoItem ammoItem, PlayerProgress progress, GiftsGenerator generator)
        {
            _generator = generator;
            base.Construct(ammoItem, progress);
        }

        protected override void Clicked()
        {
            int value = _shopAmmoStaticData.Count.GetHashCode();
            Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, value);
            ClearData();
            _generator.Clicked();
        }
    }
}