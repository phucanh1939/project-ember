using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Chase", menuName = "Game/Enemy/State/Chase")]
    public class ChaseStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Chase);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, CharacterController controller)
        {
            return new ChaseState(stateMachine, (EnemyController)controller);
        }
    }
}
