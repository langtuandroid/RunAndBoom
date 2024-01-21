using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class MobileInputService : InputService
    {
        private PlayerInput _playerInput;

        public override bool IsAttackButtonUp() => _playerInput.Player.Shoot.IsPressed();
        public override bool IsLeaderBoardButtonUp() => _playerInput.Player.LeaderBoardWindow.IsPressed();
        public override bool IsEscButtonUp() => _playerInput.Player.ESC.IsPressed();

        public override event Action<Vector2> Moved;
        public override event Action<Vector2> Looked;

        public MobileInputService(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.Enable();
        }
    }
}