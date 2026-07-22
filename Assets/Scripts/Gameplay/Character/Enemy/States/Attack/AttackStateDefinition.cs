using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Gameplay/Enemy/State/Attack")]
    public class AttackStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Attack);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new AttackState(stateMachine, (EnemyController)controller);
        }
    }
}
