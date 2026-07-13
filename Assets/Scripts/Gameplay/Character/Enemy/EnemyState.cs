namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Base class for enemy behavior states.
    ///
    /// Responsibilities:
    /// - Define state lifecycle.
    /// - Access enemy shared data through EnemyController.
    /// - Request state transitions through EnemyStateMachine.
    ///
    /// States should contain behavior logic only.
    /// They should not create other states.
    /// </summary>
    public abstract class EnemyState
    {
        protected EnemyStateMachine _stateMachine;
        protected EnemyController _controller;

        protected EnemyState(EnemyStateMachine stateMachine, EnemyController controller)
        {
            _stateMachine = stateMachine;
            _controller = controller;
        }

        protected void ChangeState(StateId stateId)
        {
            _stateMachine.ChangeState(stateId);
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }
}