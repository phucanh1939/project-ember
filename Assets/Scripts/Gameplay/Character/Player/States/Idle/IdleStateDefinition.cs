using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Game/Player/State/Idle")]
    public class IdleStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Idle);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, CharacterController controller)
        {
            return new IdleState(stateMachine, (PlayerController)controller);
        }
    }
}
