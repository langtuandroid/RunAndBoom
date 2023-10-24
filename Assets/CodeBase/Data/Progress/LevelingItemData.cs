using System;
using CodeBase.StaticData.Items;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class LevelingItemData : ItemData
    {
        public LevelTypeId LevelTypeId;

        public event Action LevelChanged;

        protected void InitNew()
        {
            LevelTypeId = LevelTypeId.None;
        }

        protected void Up()
        {
            switch (LevelTypeId)
            {
                case LevelTypeId.None:
                    LevelTypeId = LevelTypeId.Level_1;
                    LevelChanged?.Invoke();
                    break;
                case LevelTypeId.Level_1:
                    LevelTypeId = LevelTypeId.Level_2;
                    LevelChanged?.Invoke();
                    break;
                case LevelTypeId.Level_2:
                    LevelTypeId = LevelTypeId.Level_3;
                    LevelChanged?.Invoke();
                    break;
            }
        }

        protected LevelTypeId GetNextLevel()
        {
            LevelTypeId nextLevel = LevelTypeId.None;

            switch (LevelTypeId)
            {
                case LevelTypeId.None:
                    nextLevel = LevelTypeId.Level_1;
                    break;
                case LevelTypeId.Level_1:
                    nextLevel = LevelTypeId.Level_2;
                    break;
                case LevelTypeId.Level_2:
                    nextLevel = LevelTypeId.Level_3;
                    break;
            }

            return nextLevel;
        }
    }
}