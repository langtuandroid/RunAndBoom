using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class ItemGiftItem : ItemItemBase
    {
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