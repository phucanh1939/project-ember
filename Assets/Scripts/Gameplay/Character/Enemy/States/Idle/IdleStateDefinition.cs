using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Game/Enemy/State/Idle")]
    /// <summary>
    /// Creates the runtime enemy idle state.
    /// </summary>
    public class IdleStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Idle);
        }

        public override EnemyState Create(StateMachine<CharacterStateId> stateMachine, EnemyController controller)
        {
            return new IdleState(stateMachine, controller);
        }
    }
}
