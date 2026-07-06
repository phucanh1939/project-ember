public class MoveState : PlayerState
{
    public MoveState(PlayerStateMachine stateMachine)
        : base(stateMachine)
    {
    }

    public override void Update()
    {
        Controller.Movement.SetMoveDirection(Controller.Input.Move);

        if (Controller.Input.Move == Vector2.zero)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
