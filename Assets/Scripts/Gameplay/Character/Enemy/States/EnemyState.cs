using Game.Core;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Provides shared controller access and transitions for player states.
    /// </summary>
    public abstract class EnemyState : CharacterState
    {
        protected EnemyController _enemyController;

        protected EnemyState(StateMachine<CharacterStateId> stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
            _enemyController = controller;
        }
    }
}
