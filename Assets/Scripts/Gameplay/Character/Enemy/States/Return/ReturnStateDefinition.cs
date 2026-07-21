using Game.Core;
using UnityEngine;
using Game.Gameplay;

namespace Game.Gameplay.Enemy
{
    [CreateAssetMenu(fileName = "Return", menuName = "Game/Enemy/State/Return")]
    public class ReturnStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Return);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new ReturnState(stateMachine, (EnemyController)controller);
        }
    }
}
