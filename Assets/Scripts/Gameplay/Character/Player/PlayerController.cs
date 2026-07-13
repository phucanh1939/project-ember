using UnityEngine;

namespace Game.Gameplay.Player
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
    [RequireComponent(typeof(Attack))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        
        private PlayerStateMachine _stateMachine;

        public PlayerInput Input => _playerInput;
        public Movement Movement => _movement;
        public Health Health => _health;
        public Attack Attack => _attack;

        private void OnValidate()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _attack = GetComponent<Attack>();
        }

        private void Awake()
        {
            _stateMachine = new PlayerStateMachine(this);
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }

}
