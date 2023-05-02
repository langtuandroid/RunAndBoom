using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.FinishLevel.Items
{
    public class UpgradeGiftItem : UpgradeItemBase
    {
        [SerializeField] private Image PriceCrossing;
        [SerializeField] private Image Free;

        protected override void Clicked()
        {
            Progress.WeaponsData.UpgradesData.LevelUp(_upgradableWeaponStaticData.WeaponTypeId,
                _upgradeStaticData.UpgradeTypeId);
            ClearData();
        }
    }
}