using CodeBase.StaticData.Items.Shop;
using UnityEngine;

namespace CodeBase.StaticData.Items
{
    [CreateAssetMenu(fileName = "PerkData", menuName = "StaticData/Items/Shop/Perk")]
    public class PerkStaticData : BaseShopStaticData, ILeveling
    {
        public PerkTypeId PerkTypeId;
        public LevelTypeId LevelTypeId;
        public Sprite LevelImage;

        public LevelTypeId ILevelTypeId => LevelTypeId;
        public Sprite ILevel => LevelImage;
    }
}