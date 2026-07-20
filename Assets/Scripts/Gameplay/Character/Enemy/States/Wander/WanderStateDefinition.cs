using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Wander", menuName = "Game/Enemy/State/Wander")]
    /// <summary>
    /// Creates the runtime enemy wander state.
    /// </summary>
    public class WanderStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Wander);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, CharacterController controller)
        {
            return new WanderState(stateMachine, (EnemyController)controller);
        }
    }
}
