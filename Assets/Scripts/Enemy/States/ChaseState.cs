using UnityEngine;

namespace Game.Enemy
{
    public class ChaseState : EnemyState
    {
        public ChaseState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller)
        {
        }

        public override void Update()
        {
            Transform target = _controller.Target;
            if (target == null)
            {
                _stateMachine.ChangeState(StateId.Idle);
                return;
            }

            Vector2 direction = target.position - _controller.transform.position;

            _controller.Movement.SetMoveDirection(direction.normalized);
        }


        public override void Exit()
        {
            _controller.Movement.SetMoveDirection(Vector2.zero);
        }
    }
}