using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    [CreateAssetMenu(fileName = "Move", menuName = "Game/Player/State/Move")]
    public class MoveStateDefinition : CharacterStateDefinition
    {
        private void OnEnable()
        {
            SetId(CharacterStateId.Move);
        }

        public override CharacterState Create(StateMachine<CharacterStateId> stateMachine, CharacterController controller)
        {
            return new IdleState(stateMachine, (PlayerController)controller);
        }
    }
}
