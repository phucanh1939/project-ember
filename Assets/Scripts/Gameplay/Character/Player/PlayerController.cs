using UnityEngine;

namespace Game.Gameplay.Player
{
    /// <summary>
    /// Coordinates player components and initializes player behavior.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(StatModifierContainer))]
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
        [SerializeField] private CharacterStats _stats;
        [SerializeField] private StatModifierContainer _statModiferContainer;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private StatusEffect _statusEffect;
        [SerializeField] private PlayerBehavior _behavior;

        public PlayerInput Input => _playerInput;
        public Movement Movement => _movement;
        public Health Health => _health;
        public Attack Attack => _attack;
        public CharacterStats Stats => _stats;
        public StatModifierContainer StatModifierContainer => _statModiferContainer;
        public StatusEffect StatusEffect => _statusEffect;
        public PlayerBehavior PlayerBehavior => _behavior;

        private void OnValidate()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movement = GetComponent<Movement>();
            _stats = GetComponent<CharacterStats>();
            _statModiferContainer = GetComponent<StatModifierContainer>();
            _health = GetComponent<Health>();
            _attack = GetComponent<Attack>();
            _statusEffect = GetComponent<StatusEffect>();
            _behavior = GetComponent<PlayerBehavior>();
        }

        private void Awake() => _behavior.Initialize(this);
    }
}
