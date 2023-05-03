using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class WeaponGiftItem : WeaponItemBase
    {
        protected override void Clicked()
        {
            Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
            ClearData();
        }
    }
}