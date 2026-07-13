using Game.Core;

namespace Game.Gameplay.Enemy
{
    public abstract class EnemyState : State<StateId>
    {
        protected EnemyController _controller;

        protected EnemyState(StateMachine<StateId> stateMachine, EnemyController controller) : base(stateMachine)
        {
            _controller = controller;
        }
    }
}
