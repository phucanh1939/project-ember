using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// Enemy state responsible for pursuing the current target.
    ///
    /// Responsibilities:
    /// - Move the enemy toward the current target.
    /// - Check whether the target is within attack range.
    /// - Stop chasing when the target is lost or too far from home.
    /// - Transition to AttackState when the enemy can attack.
    /// - Transition to ReturnState when the enemy abandons the chase.
    ///
    /// This state does NOT:
    /// - Find new targets.
    /// - Perform attacks.
    /// - Handle damage or combat logic.
    ///
    /// Target acquisition is handled by:
    /// - IdleState
    /// - WanderState
    /// - Sensor
    ///
    /// The target is provided through EnemyController.
    /// </summary>
    public class ChaseState : EnemyState
    {
        // TODO Attack range and leash range should be configurable per enemy type.
        private const float _attackRange = 1.5f;
        private const float _attackRangeSquare = _attackRange * _attackRange;
        private const float _leashRange = 10f;
        private const float _leashRangeSquare = _leashRange * _leashRange;


        public ChaseState(
            EnemyStateMachine stateMachine,
            EnemyController controller)
            : base(stateMachine, controller)
        {
        }


        public override void Enter()
        {
            // Reset previous movement command when entering this state.
            // Prevents leftover movement from the previous state during transitions.
            _controller.Movement.StopMovement();
        }


        public override void Update()
        {
            Transform target = _controller.Target;

            if (target == null)
            {
                ChangeState(StateId.Return);
                return;
            }

            Vector2 fromSpawn = (Vector2)_controller.transform.position - _controller.SpawnPosition;

            if (fromSpawn.sqrMagnitude > _leashRangeSquare)
            {
                ChangeState(StateId.Return);
                return;
            }


            Vector2 toTarget = target.position - _controller.transform.position;


            if (toTarget.sqrMagnitude <= _attackRangeSquare)
            {
                ChangeState(StateId.Attack);
                return;
            }


            _controller.Movement.SetMoveDirection(toTarget.normalized);
        }


        public override void Exit()
        {
            // Clear movement command when leaving this state.
            // Prevents the enemy from continuing to move after this state is no longer active.
            _controller.Movement.StopMovement();
        }
    }
}