using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.StaticData.Items.Shop.Items;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class ItemGiftItem : ItemItemBase
    {
        private GiftsGenerator _generator;

        // public void Construct(ItemTypeId typeId, PlayerProgress progress, GameObject hero, GiftsGenerator generator)
        public void Construct(ItemTypeId typeId, PlayerProgress progress, HeroHealth health, GiftsGenerator generator)
        {
            _generator = generator;
            // base.Construct(typeId, progress, hero);
            base.Construct(typeId, progress, health);
        }

        protected override void Clicked()
        {
            if (_itemStaticData.TypeId == ItemTypeId.HealthRecover)
                // Hero.GetComponent<HeroHealth>().Recover();
                Health.Recover();

            ClearData();
            _generator.Clicked();
        }
    }
}