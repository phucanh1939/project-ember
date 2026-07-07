using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// IdleState is a PlayerState that handles the player being idle.
    ///
    /// The IdleState reads input from the PlayerInput component and
    /// transitions to the MoveState when movement input is detected.
    /// </summary>
    public class IdleState : PlayerState
    {
        public IdleState(PlayerStateMachine stateMachine)
            : base(stateMachine)
        {
        }

        public override void Update()
        {
            Controller.Movement.SetMoveDirection(Vector2.zero);

            if (Controller.Input.Move != Vector2.zero)
            {
                StateMachine.ChangeState(StateMachine.MoveState);
            }
        }
    }

}
