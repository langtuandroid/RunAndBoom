using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.FinishLevel.Items
{
    public class AmmoGiftItem : AmmoItemBase
    {
        [SerializeField] private Image PriceCrossing;
        [SerializeField] private Image Free;

        protected override void Clicked()
        {
            int value = _shopAmmoStaticData.Count.GetHashCode();
            Progress.WeaponsData.WeaponsAmmoData.AddAmmo(_ammoItem.WeaponTypeId, value);
            ClearData();
        }
    }
}