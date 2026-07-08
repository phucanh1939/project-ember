namespace Game.Enemy
{
    public abstract class EnemyState
    {
        protected EnemyStateMachine StateMachine { get; }

        protected EnemyController Controller => StateMachine.Controller;

        protected EnemyState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }
    }
}