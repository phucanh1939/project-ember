using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Handles player attack state.
    ///
    /// Starts an attack and waits until the attack finishes.
    /// </summary>
    public class AttackState : PlayerState
    {
        public AttackState(StateMachine<CharacterStateId> stateMachine, PlayerController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _controller.Attack.OnAttackEnded += HandleAttackEnded;
            _controller.Attack.StartAttack();
        }

        public override void Exit()
        {
            _controller.Attack.OnAttackEnded -= HandleAttackEnded;
            _controller.Attack.CancelAttackIfActive();
        }

        private void HandleAttackEnded()
        {
            _stateMachine.ChangeState(CharacterStateId.Idle);
        }
    }
}