using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Wander", menuName = "Game/Enemy/State/Wander")]
    /// <summary>
    /// Creates the runtime enemy wander state.
    /// </summary>
    public class WanderStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Wander);
        }

        public override EnemyState Create(StateMachine<CharacterStateId> stateMachine, EnemyController controller)
        {
            return new WanderState(stateMachine, controller);
        }
    }
}
