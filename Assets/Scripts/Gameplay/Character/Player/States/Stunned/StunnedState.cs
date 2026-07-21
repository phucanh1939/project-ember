using Game.Core;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Character state while affected by a stun interrupt.
    ///
    /// Normal character actions are disabled while active.
    /// The state lifetime is controlled by the interrupt system.
    /// </summary>
    public class StunnedState : PlayerState
    {
        private readonly Movement _movement;

        public StunnedState(
            StateMachine<CharacterStateId> stateMachine,
            PlayerController controller)
            : base(stateMachine, controller)
        {
            _movement = controller.Movement;
        }

        public override void Enter()
        {
            _movement.StopMovement();
        }


        public override void Exit()
        {
            // Nothing to restore.
            // The next state will decide the character behavior (state interruptor).
        }
    }
}