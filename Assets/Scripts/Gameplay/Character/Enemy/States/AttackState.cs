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
        public AttackState(EnemyStateMachine stateMachine, EnemyController controller)
            : base(stateMachine, controller)
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
            ChangeState(StateId.Chase);
        }
    }
}