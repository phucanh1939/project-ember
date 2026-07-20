using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Game/Enemy/State/Attack")]
    /// <summary>
    /// Creates the runtime enemy attack state.
    /// </summary>
    public class AttackStateDefinition : StateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Attack);
        }

        public override EnemyState Create(StateMachine<CharacterStateId> stateMachine, EnemyController controller)
        {
            return new AttackState(stateMachine, controller);
        }
    }
}
