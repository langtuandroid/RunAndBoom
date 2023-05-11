using System;
using CodeBase.Data;

namespace CodeBase.StaticData
{
    [Serializable]
    public class LevelTransferStaticData
    {
        public Scene TransferTo;

        public LevelTransferStaticData(Scene transferTo)
        {
            TransferTo = transferTo;
        }
    }
}