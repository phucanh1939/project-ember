using Game.Core;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Provides shared controller access and transitions for player states.
    /// </summary>
    public abstract class PlayerState : State<CharacterStateId>
    {
        protected PlayerController _controller;

        protected PlayerState(StateMachine<CharacterStateId> stateMachine, PlayerController controller) : base(stateMachine)
        {
            _controller = controller;
        }

        protected bool TryAttack()
        {
            if (!_controller.Input.AttackPressed)
                return false;

            _stateMachine.ChangeState(CharacterStateId.Attack);
            return true;
        }
    }
}
