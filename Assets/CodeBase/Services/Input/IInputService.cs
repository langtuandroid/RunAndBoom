using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        bool IsAttackButtonUp();
        bool IsLeaderBoardButtonUp();
        bool IsEscButtonUp();

        public event Action<Vector2> Moved;
        public event Action<Vector2> Looked;
    }
}