using Game.Core;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Provides shared controller access for enemy states.
    /// </summary>
    public abstract class EnemyState : State<CharacterStateId>
    {
        protected EnemyController _controller;

        protected EnemyState(StateMachine<CharacterStateId> stateMachine, EnemyController controller) : base(stateMachine)
        {
            _controller = controller;
        }
    }
}
