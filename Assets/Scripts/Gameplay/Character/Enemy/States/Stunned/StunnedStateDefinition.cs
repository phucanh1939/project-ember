using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Stunned", menuName = "Gameplay/Enemy/State/Stunned")]
    public class StunnedStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Stunned);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new StunnedState(stateMachine, (EnemyController)controller);
        }
    }
}
