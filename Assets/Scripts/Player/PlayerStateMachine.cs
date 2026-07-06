public class PlayerStateMachine : MonoBehaviour
{
    private PlayerController _controller;

    private IdleState _idleState;
    private MoveState _moveState;

    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerController controller)
    {
        _controller = controller;

        _idleState = new IdleState(this);
        _moveState = new MoveState(this);

        ChangeState(_idleState);
    }

    public void ChangeState(PlayerState nextState)
    {
        if (CurrentState == nextState)
            return;

        CurrentState?.Exit();

        CurrentState = nextState;

        CurrentState.Enter();
    }

    private void Update()
    {
        CurrentState?.Update();
    }

    public PlayerController Controller => _controller;

    public IdleState IdleState => _idleState;
    public MoveState MoveState => _moveState;
}
