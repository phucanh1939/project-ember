using UnityEngine;
using Game.Gameplay;
using Game.Core;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Enemy roaming state.
    ///
    /// Behavior:
    /// - Move toward a random position around spawn location.
    /// - Enable Sensor to detect possible targets.
    ///
    /// Transitions:
    /// - Wander -> Chase: Target detected by Sensor.
    /// - Wander -> Idle: Destination reached.
    /// </summary>
    public class WanderState : EnemyState
    {
        // TODO: Move these values to WanderStateDefinition.
        // Different enemy types should have different wander settings.
        private const float WanderRadius = 5f;
        private const float ArriveDistance = 0.2f;
        private const float ArriveDistanceSqr = ArriveDistance * ArriveDistance;

        private Vector2 _destination;

        public WanderState(StateMachine<StateId> stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _controller.Sensor.SetMode(SensorMode.Passive);
            Vector2 randomOffset = Random.insideUnitCircle * WanderRadius;
            _destination = (Vector2)_controller.SpawnPosition + randomOffset;
        }


        public override void Update()
        {
            Collider2D target = _controller.Sensor.DetectedObject;

            if (target != null)
            {
                _controller.SetTarget(target.transform);
                _stateMachine.ChangeState(StateId.Chase);
                return;
            }

            Vector2 direction = _destination - (Vector2)_controller.transform.position;

            if (direction.sqrMagnitude <= ArriveDistanceSqr)
            {
                _stateMachine.ChangeState(StateId.Idle);
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