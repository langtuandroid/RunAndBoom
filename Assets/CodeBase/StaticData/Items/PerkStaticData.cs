using CodeBase.StaticData.Items.Shop;
using CodeBase.StaticData.Items.Shop.WeaponsUpgrades;
using UnityEngine;

namespace CodeBase.StaticData.Items
{
    [CreateAssetMenu(fileName = "PerkData", menuName = "StaticData/Items/Shop/Perk")]
    public class PerkStaticData : BaseShopStaticData
    {
        public PerkTypeId PerkTypeId;
        public UpgradeLevelTypeId LevelTypeId;
    }
}