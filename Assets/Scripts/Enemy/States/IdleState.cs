using UnityEngine;

namespace Game.Enemy
{
    public class IdleState : EnemyState
    {
        public IdleState(EnemyStateMachine stateMachine)
            : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Controller.Movement.SetMoveDirection(Vector2.zero);
        }
    }
}