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
    [RequireComponent(typeof(StatusEffect))]
    [RequireComponent(typeof(PlayerBehavior))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private StatusEffect _statusEffect;
        [SerializeField] private PlayerBehavior _behavior;

        public PlayerInput Input => _playerInput;
        public Movement Movement => _movement;
        public Health Health => _health;
        public Attack Attack => _attack;
        public StatusEffect StatusEffect => _statusEffect;
        public PlayerBehavior PlayerBehavior => _behavior;

        private void OnValidate()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _attack = GetComponent<Attack>();
            _statusEffect = GetComponent<StatusEffect>();
            _behavior = GetComponent<PlayerBehavior>();
        }

        private void Awake()
        {
            _behavior.Initialize(this);
        }
    }

}
