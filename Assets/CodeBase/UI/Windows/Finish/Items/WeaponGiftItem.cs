using CodeBase.Data;
using CodeBase.StaticData.Weapons;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish.Items
{
    public class WeaponGiftItem : WeaponItemBase
    {
        private GiftsGenerator _generator;

        public void Construct(HeroWeaponTypeId weaponTypeId, PlayerProgress progress, GiftsGenerator generator)
        {
            _generator = generator;
            base.Construct(weaponTypeId, progress);
        }

        protected override void Clicked()
        {
            Progress.WeaponsData.SetAvailableWeapon(_weaponTypeId);
            ClearData();
            _generator.Clicked();
        }
    }
}