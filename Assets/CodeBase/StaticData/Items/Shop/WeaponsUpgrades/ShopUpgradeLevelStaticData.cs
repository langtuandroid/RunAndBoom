using UnityEngine;

namespace CodeBase.StaticData.Items.Shop.WeaponsUpgrades
{
    [CreateAssetMenu(fileName = "ShopUpgradeLevelData", menuName = "StaticData/Items/Shop/UpgradeLevel")]
    public class ShopUpgradeLevelStaticData : BaseItemStaticData
    {
        public LevelTypeId LevelTypeId;

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