using Game.Core;

namespace Game.Gameplay.Player
{
    public abstract class PlayerState : State<StateId>
    {
        protected PlayerController _controller;

        protected PlayerState(StateMachine<StateId> stateMachine, PlayerController controller) : base(stateMachine)
        {
            _controller = controller;
        }

        protected bool TryAttack()
        {
            if (!_controller.Input.AttackPressed)
                return false;

            _stateMachine.ChangeState(StateId.Attack);
            return true;
        }
    }
}
