using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Return", menuName = "Game/Enemy/State/Return")]
    /// <summary>
    /// Creates the runtime enemy return state.
    /// </summary>
    public class ReturnStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Return);
        }

        public override EnemyState Create(StateMachine<CharacterStateId> stateMachine, EnemyController controller)
        {
            return new ReturnState(stateMachine, controller);
        }
    }
}
