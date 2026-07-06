using UnityEngine;

/// <summary>
/// Coordinates the player's gameplay components.
///
/// The PlayerController owns the player's gameplay components and
/// initializes the PlayerStateMachine.
///
/// Once initialized, gameplay decisions are delegated to the current
/// player state.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private Movement _movement;

    [SerializeField]
    private PlayerStateMachine _stateMachine;

    public PlayerInput Input => _playerInput;
    public Movement Movement => _movement;

    private void Awake()
    {
        _stateMachine.Initialize(this);
    }
}
