using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    [CreateAssetMenu(fileName = "Dead", menuName = "Gameplay/Player/State/Dead")]
    public class DeadStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Dead);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, Character controller)
        {
            return new DeadState(stateMachine, (PlayerController)controller);
        }
    }
}
