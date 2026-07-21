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
    public class DeadState : PlayerState
    {
        public DeadState(StateMachine<CharacterStateId> stateMachine, PlayerController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
        }

    }

}
