using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// MoveState is a PlayerState that handles player movement.
    ///
    /// The MoveState reads input from the PlayerInput component and
    /// delegates movement to the Movement component.
    /// </summary>
    public class MoveState : PlayerState
    {
        public MoveState(PlayerStateMachine stateMachine, PlayerController controller)
            : base(stateMachine, controller)
        {
        }

        public override void Update()
        {
            _controller.Movement.SetMoveDirection(_controller.Input.Move);

            if (TryAttack()) return;

            if (_controller.Input.Move == Vector2.zero)
            {
                _stateMachine.ChangeState(StateId.Idle);
            }
        }
    }

}
