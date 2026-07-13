using UnityEngine;
using Game.Gameplay;

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


        public IdleState(
            EnemyStateMachine stateMachine,
            EnemyController controller)
            : base(stateMachine, controller)
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
            Collider2D target = _controller.Sensor.DetectedObject;

            if (target != null)
            {
                _controller.SetTarget(target.transform);
                ChangeState(StateId.Chase);
                return;
            }


            if (Time.time >= _idleEndTime)
            {
                ChangeState(StateId.Wander);
            }
        }


        public override void Exit()
        {
        }
    }
}