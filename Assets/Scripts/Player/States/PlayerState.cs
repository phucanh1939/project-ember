namespace Game.Player
{
    public abstract class PlayerState
    {
        protected PlayerStateMachine _stateMachine;
        protected PlayerController _controller;

        protected PlayerState(PlayerStateMachine stateMachine, PlayerController controller)
        {
            _stateMachine = stateMachine;
            _controller = controller;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        protected bool TryAttack()
        {
            if (!_controller.Input.AttackPressed)
                return false;

            _stateMachine.ChangeState(StateId.Attack);
            return true;
        }
    }
}
