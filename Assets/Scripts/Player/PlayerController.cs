using UnityEngine;
using Game.Gameplay;

namespace Game.Player
{
    /// <summary>
    /// Coordinates the player's gameplay components.
    ///
    /// The PlayerController owns the player's gameplay components and
    /// initializes the PlayerStateMachine.
    ///
    /// Once initialized, gameplay decisions are delegated to the current
    /// player state.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(PlayerStateMachine))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private PlayerStateMachine _stateMachine;

        public PlayerInput Input => _playerInput;
        public Movement Movement => _movement;
        public Health Health => _health;

        private void OnValidate()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _stateMachine = GetComponent<PlayerStateMachine>();
        }

        private void Awake()
        {
            _stateMachine.Initialize(this);
        }
    }

}
