using UnityEngine;

namespace CodeBase
{
    public static class Constants
    {
        public const int InitialMaxHp = 10;
        public const float AdditionYToEnemy = 1f;
        public const float AlphaActiveItem = 1f;
        public const float AlphaTutorialItem = 0.5f;
        public const float AlphaInactiveItem = 0f;
        public const float Zero = 0.0f;

        public static readonly Color ShopItemUpgrade = new Color(1f, 0.7064719f, 0f, 1f);
        public static readonly Color ShopItemAmmo = new Color(0.2321882f, 1f, 0f, 1f);
        public static readonly Color ShopItemWeapon = new Color(0.8896403f, 1f, 0f, 1f);
        public static readonly Color ShopItemPerk = new Color(0f, 0.7270764f, 0.9433962f, 1f);
        public static readonly Color ShopItemItem = new Color(0.7459788f, 0f, 1f, 1f);

        public const string HeroTag = "Hero";
        public const string WallTag = "Wall";
        public const string EnemyTag = "Enemy";
        public const string ObstacleTag = "Obstacle";
        public const string DestructableTag = "Destructable";
        public const string GroundTag = "Ground";
        public const string Level = "Level_";
    }
}