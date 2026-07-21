using Game.Core;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Provides shared controller access and transitions for player states.
    /// </summary>
    public abstract class PlayerState : CharacterState
    {
        protected PlayerController _playerController;

        protected PlayerState(StateMachine<CharacterStateId> stateMachine, PlayerController controller) : base(stateMachine, controller)
        {
            _playerController = controller;
        }

        protected bool TryAttack()
        {
            if (!_playerController.Input.AttackPressed)
                return false;

            _stateMachine.ChangeState(CharacterStateId.Attack);
            return true;
        }
    }
}
