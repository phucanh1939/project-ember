using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// IdleState is a PlayerState that handles the player being idle.
    ///
    /// The IdleState reads input from the PlayerInput component and
    /// transitions to the MoveState when movement input is detected.
    /// </summary>
    public class IdleState : PlayerState
    {
        public IdleState(StateMachine<CharacterStateId> stateMachine, PlayerController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _controller.Movement.StopMovement();
        }

        public override void Update()
        {
            if (TryAttack()) return;

            if (_controller.Input.Move != Vector2.zero)
            {
                _stateMachine.ChangeState(CharacterStateId.Move);
            }
        }
    }

}
