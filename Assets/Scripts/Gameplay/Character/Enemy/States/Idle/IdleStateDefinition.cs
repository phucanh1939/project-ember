using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Gameplay/Enemy/State/Idle")]
    public class IdleStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Idle);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new IdleState(stateMachine, (EnemyController)controller);
        }
    }
}
