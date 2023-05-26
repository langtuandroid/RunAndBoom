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
        [Range(0f, 2f)] public float Value;

        public LevelTypeId ILevelTypeId => LevelTypeId;
        public Sprite ILevel => LevelImage;

        [HideInInspector]
        public string Level
        {
            get
            {
                if (LevelTypeId != LevelTypeId.None)
                    return LevelTypeId.ToString().Replace(Constants.Level, "");
                else
                    return "";
            }
        }
    }
}