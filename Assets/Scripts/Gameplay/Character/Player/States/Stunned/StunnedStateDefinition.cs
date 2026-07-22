using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    [CreateAssetMenu(fileName = "Stunned", menuName = "Gameplay/Player/State/Stunned")]
    public class StunnedStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Stunned);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new StunnedState(stateMachine, (PlayerController)controller);
        }
    }
}
