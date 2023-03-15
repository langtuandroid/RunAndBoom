using UnityEngine;

namespace CodeBase.StaticData.Items
{
    public interface ILeveling
    {
        public LevelTypeId ILevelTypeId { get; }
        public Sprite ILevel { get; }
    }
}