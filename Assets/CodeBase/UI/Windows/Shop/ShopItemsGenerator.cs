using System;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopItemsGenerator : ItemsGeneratorBase
    {
        public override event Action GenerationStarted;
        public override event Action GenerationEnded;

        protected override void CreateAllItems()
        {
            CreateNextLevelPerks();
            CreateNextLevelUpgrades();
            CreateAmmunition();
            CreateWeapons();
            CreateItems();
        }

        protected override void GenerateAllItems()
        {
            GenerateItems();
            GenerateAmmo();
            GeneratePerks();
            GenerateUpgrades();
            GenerateWeapons();
        }
    }
}