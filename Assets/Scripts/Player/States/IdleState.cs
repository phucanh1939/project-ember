public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine)
        : base(stateMachine)
    {
    }

    public override void Update()
    {
        Controller.Movement.SetMoveDirection(Vector2.zero);

        if (Controller.Input.Move != Vector2.zero)
        {
            StateMachine.ChangeState(StateMachine.MoveState);
        }
    }
}
