using Game.Core;
using UnityEngine;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Enemy recovery state.
    ///
    /// Behavior:
    /// - Move back to the original spawn position.
    /// - Clear current target.
    ///
    /// Transitions:
    /// - Return -> Idle: Spawn position reached.
    /// </summary>
    public class ReturnState : EnemyState
    {
        // TODO: Move this value to ReturnStateDefinition.
        // Different enemies may have different arrival distances.
        private readonly float _arriveDistance = 0.2f;

        public ReturnState(StateMachine<CharacterStateId> stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _enemyController.SetTarget(null);
            _enemyController.Movement.StopMovement();
        }

        public override void Update()
        {
            Vector2 direction = _enemyController.SpawnPosition - (Vector2)_enemyController.transform.position;

            // TODO cache squared distance _arriveDistance * _arriveDistance
            if (direction.sqrMagnitude <= _arriveDistance * _arriveDistance)
            {
                _stateMachine.ChangeState(CharacterStateId.Idle);
                return;
            }

            _enemyController.Movement.SetMoveDirection(direction.normalized);
        }

        public override void Exit()
        {
            _enemyController.Movement.StopMovement();
        }

    }
}