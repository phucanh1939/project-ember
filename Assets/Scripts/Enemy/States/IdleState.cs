using UnityEngine;

namespace Game.Enemy
{
    public class IdleState : EnemyState
    {
        private readonly float _scanInterval = 0.25f;
        private float _nextScanTime;

        public IdleState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
        }

        public override void Enter()
        {
            _controller.Movement.SetMoveDirection(Vector2.zero);
            _nextScanTime = Time.time;
        }

        public override void Update()
        {
            if (Time.time < _nextScanTime)
                return;

            _nextScanTime = Time.time + _scanInterval;

            Collider2D target = _controller.Sensor.Scan(_controller.TargetMask);

            if (target == null)
                return;

            _controller.SetTarget(target.transform);

            _stateMachine.ChangeState(StateId.Chase);
        }
    }
}