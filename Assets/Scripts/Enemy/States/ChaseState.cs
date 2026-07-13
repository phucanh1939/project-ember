using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// Enemy pursuit state.
    ///
    /// Behavior:
    /// - Move toward the current target.
    /// - Follow target until attack range or chase is abandoned.
    ///
    /// Transitions:
    /// - Chase -> Attack: Target is within attack range.
    /// - Chase -> Return: Target lost or exceeds leash range.
    /// </summary>
    public class ChaseState : EnemyState
    {
        // TODO: Move these values to enemy configuration.
        private const float AttackRange = 1.5f;
        private const float AttackRangeSquare = AttackRange * AttackRange;
        private const float LeashRange = 10f;
        private const float LeashRangeSquare = LeashRange * LeashRange;

        public ChaseState(
            EnemyStateMachine stateMachine,
            EnemyController controller)
            : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _controller.Movement.StopMovement();
        }

        public override void Update()
        {
            if (ShouldReturn())
            {
                ChangeState(StateId.Return);
                return;
            }

            if (ShouldAttack())
            {
                ChangeState(StateId.Attack);
                return;
            }

            MoveToTarget();
        }

        public override void Exit()
        {
            _controller.Movement.StopMovement();
        }

        private bool ShouldReturn()
        {
            Transform target = _controller.Target;

            if (target == null)
                return true;

            Vector2 fromSpawn = (Vector2)_controller.transform.position - _controller.SpawnPosition;

            return fromSpawn.sqrMagnitude > LeashRangeSquare;
        }

        private bool ShouldAttack()
        {
            Vector2 toTarget = _controller.Target.position - _controller.transform.position;

            return toTarget.sqrMagnitude <= AttackRangeSquare;
        }

        private void MoveToTarget()
        {
            Vector2 direction = _controller.Target.position - _controller.transform.position;

            _controller.Movement.SetMoveDirection(direction.normalized);
        }
    }
}