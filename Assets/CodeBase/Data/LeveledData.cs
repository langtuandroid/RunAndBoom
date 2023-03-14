using System;
using CodeBase.StaticData.Items;

namespace CodeBase.Data
{
    [Serializable]
    public class LeveledData
    {
        public LevelTypeId LevelTypeId { get; private set; }

        public event Action LevelChanged;

        protected LeveledData()
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
    }
}