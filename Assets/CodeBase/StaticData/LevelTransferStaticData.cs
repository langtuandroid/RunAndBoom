using UnityEngine;

namespace CodeBase.StaticData
{
    public class LevelTransferStaticData
    {
        public string TransferTo;
        public Vector3 Position;

        public LevelTransferStaticData(string transferTo, Vector3 position)
        {
            TransferTo = transferTo;
            Position = position;
        }
    }
}