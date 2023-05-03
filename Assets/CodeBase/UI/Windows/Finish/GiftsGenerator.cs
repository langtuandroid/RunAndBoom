using System;
using CodeBase.UI.Windows.Common;

namespace CodeBase.UI.Windows.Finish
{
    public class GiftsGenerator : ItemsGeneratorBase
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
            CreateMoney();
        }

        protected override void GenerateAllItems()
        {
            GenerateItems();
            GenerateAmmo();
            GeneratePerks();
            GenerateUpgrades();
            GenerateWeapons();
            GenerateMoney();
        }
    }
}