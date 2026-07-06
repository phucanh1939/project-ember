using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// MoveState is a PlayerState that handles player movement.
    ///
    /// The MoveState reads input from the PlayerInput component and
    /// delegates movement to the Movement component.
    /// </summary>
    public class MoveState : PlayerState
    {
        public MoveState(PlayerStateMachine stateMachine)
            : base(stateMachine)
        {
        }

        public override void Update()
        {
            Controller.Movement.SetMoveDirection(Controller.Input.Move);

            if (Controller.Input.Move == Vector2.zero)
            {
                StateMachine.ChangeState(StateMachine.IdleState);
            }
        }
    }

}
