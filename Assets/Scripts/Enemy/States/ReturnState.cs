using UnityEngine;

namespace Game.Enemy
{
    /// <summary>
    /// Enemy state responsible for returning to its original position.
    ///
    /// Responsibilities:
    /// - Move the enemy back to its spawn position.
    /// - Clear the current target after abandoning the chase.
    /// - Transition back to IdleState when reaching home.
    ///
    /// This state does NOT:
    /// - Find targets.
    /// - Chase enemies.
    /// - Handle combat.
    ///
    /// Return behavior happens when:
    /// - The target is lost.
    /// - The enemy exceeds its leash range.
    /// </summary>
    public class ReturnState : EnemyState
    {
        // TODO: Move this value to ReturnStateDefinition.
        // Different enemies may have different arrival distances.
        private readonly float _arriveDistance = 0.2f;


        public ReturnState(
            EnemyStateMachine stateMachine,
            EnemyController controller)
            : base(stateMachine, controller)
        {
        }


        public override void Enter()
        {
            _controller.SetTarget(null);

            _controller.Movement.StopMovement();
        }


        public override void Update()
        {
            Vector2 direction = _controller.SpawnPosition - (Vector2)_controller.transform.position;

            // TODO cache squared distance _arriveDistance * _arriveDistance
            if (direction.sqrMagnitude <= _arriveDistance * _arriveDistance)
            {
                ChangeState(StateId.Idle);
                return;
            }


            _controller.Movement.SetMoveDirection(direction.normalized);
        }


        public override void Exit()
        {
            _controller.Movement.StopMovement();
        }
    }
}