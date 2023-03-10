using UnityEngine;

namespace CodeBase.StaticData.Items.Shop
{
    public class BaseShopStaticData : ScriptableObject
    {
        [Range(1, 50)] public int Cost;

        public Sprite MainImage;
        public string Title;
    }
}