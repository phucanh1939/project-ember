using Game.Core;
using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Enemy attack state.
    ///
    /// Behavior:
    /// - Starts an attack against the current target.
    /// - Waits until the attack finishes.
    ///
    /// Transitions:
    /// - Attack -> Chase: Attack finished.
    /// - Attack -> Other: Forced interruption.
    /// </summary>
    public class AttackState : EnemyState
    {
        public AttackState(StateMachine<CharacterStateId> stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _enemyController.Attack.OnAttackEnded += HandleAttackEnded;
            _enemyController.Attack.StartAttack();
        }

        public override void Exit()
        {
            _enemyController.Attack.OnAttackEnded -= HandleAttackEnded;
            _enemyController.Attack.CancelAttackIfActive();
        }

        private void HandleAttackEnded()
        {
            _stateMachine.ChangeState(CharacterStateId.Chase);
        }
    }
}