using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Chase", menuName = "Game/Enemy/State/Chase")]
    /// <summary>
    /// Creates the runtime enemy chase state.
    /// </summary>
    public class ChaseStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Chase);
        }

        public override EnemyState Create(StateMachine<CharacterStateId> stateMachine, EnemyController controller)
        {
            return new ChaseState(stateMachine, controller);
        }
    }
}
