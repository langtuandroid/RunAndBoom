using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class AmmoGiftItem : AmmoItemBase
    {
        protected override void Clicked()
        {
            int value = _shopAmmoStaticData.Count.GetHashCode();
            Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, value);
            ClearData();
        }
    }
}