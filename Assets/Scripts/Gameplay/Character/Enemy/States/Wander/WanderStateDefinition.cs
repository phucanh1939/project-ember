using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Wander", menuName = "Gameplay/Enemy/State/Wander")]
    /// <summary>
    /// Creates the runtime enemy wander state.
    /// </summary>
    public class WanderStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Wander);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new WanderState(stateMachine, (EnemyController)controller);
        }
    }
}
