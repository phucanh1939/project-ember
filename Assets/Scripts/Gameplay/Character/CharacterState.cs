using Game.Core;

namespace Game.Gameplay
{
    /// <summary>
    /// Provides shared controller access for enemy states.
    /// </summary>
    public abstract class CharacterState : State<CharacterStateId>
    {
        protected CharacterController _characterController;

        protected CharacterState(StateMachine<CharacterStateId> stateMachine, CharacterController controller) : base(stateMachine)
        {
            _characterController = controller;
        }
    }
}
