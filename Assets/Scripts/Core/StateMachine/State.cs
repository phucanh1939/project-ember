namespace Game.Core
{
    public abstract class State<TStateId> where TStateId : System.Enum
    {
        protected StateMachine<TStateId> _stateMachine;

        protected State(StateMachine<TStateId> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        /// <summary>
        /// Called when entering this state.
        /// </summary>
        public virtual void Enter()
        {
        }

        /// <summary>
        /// Called every frame while this state is active.
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Called before leaving this state.
        /// </summary>
        public virtual void Exit()
        {
        }
    }
}