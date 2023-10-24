using System;
using CodeBase.Data.Progress;

namespace CodeBase.StaticData
{
    [Serializable]
    public class LevelTransferStaticData
    {
        public SceneId TransferTo;

        public LevelTransferStaticData(SceneId transferTo)
        {
            TransferTo = transferTo;
        }
    }
}