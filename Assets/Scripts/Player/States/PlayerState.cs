namespace Game.Player
{
    public abstract class PlayerState
    {
        protected PlayerStateMachine StateMachine { get; }

        protected PlayerController Controller => StateMachine.Controller;

        protected PlayerState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }
    }
}
