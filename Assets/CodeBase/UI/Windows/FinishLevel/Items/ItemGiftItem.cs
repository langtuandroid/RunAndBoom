using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.FinishLevel.Items
{
    public class ItemGiftItem : ItemItemBase
    {
        [SerializeField] private Image PriceCrossing;
        [SerializeField] private Image Free;

        protected override void Clicked()
        {
            if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
            {
                Progress.HealthState.ChangeCurrentHP(Progress.HealthState.BaseMaxHp);
                _health.ChangeHealth();
            }

            ClearData();
        }
    }
}