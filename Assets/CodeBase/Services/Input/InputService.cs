using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract bool IsAttackButtonUp();
        public abstract bool IsLeaderBoardButtonUp();
        public abstract bool IsEscButtonUp();

        public abstract event Action<Vector2> Moved;
        public abstract event Action<Vector2> Looked;
    }
}