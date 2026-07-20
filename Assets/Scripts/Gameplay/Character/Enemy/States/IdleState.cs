using UnityEngine;
using Game.Gameplay;
using Game.Core;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Enemy default inactive state.
    ///
    /// Behavior:
    /// - Enemy stays in place.
    /// - Enable Sensor to detect possible targets.
    ///
    /// Transitions:
    /// - Idle -> Chase: Target detected by Sensor.
    /// - Idle -> Wander: Idle duration has elapsed.
    /// </summary>
    public class IdleState : EnemyState
    {
        private readonly float _minIdleTime = 2f;
        private readonly float _maxIdleTime = 5f;

        private float _idleEndTime;

        public IdleState(StateMachine<StateId> stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _controller.Movement.StopMovement();
            _controller.Sensor.SetMode(SensorMode.Passive);
            _idleEndTime = Time.time + Random.Range(_minIdleTime, _maxIdleTime);
        }

        public override void Update()
        {
            var target = _controller.Sensor.DetectedObject;

            if (ShouldChase(target))
            {
                _controller.SetTarget(target.transform);
                _stateMachine.ChangeState(StateId.Chase);
                return;
            }

            if (ShouldWander())
            {
                _stateMachine.ChangeState(StateId.Wander);
                return;
            }
        }

        private bool ShouldChase(Collider2D target)
        {
            return target != null;
        }

        private bool ShouldWander()
        {
            return Time.time >= _idleEndTime;
        }

    }
}