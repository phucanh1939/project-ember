using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Coordinates the character's gameplay components.
    ///
    /// Gameplay behavior is delegated to the current state.
    /// The controller stores shared data required by states.
    /// </summary>
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Attack))]
    [RequireComponent(typeof(CharacterBehavior))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(StatModifierContainer))]
    [RequireComponent(typeof(StatusEffectController))]
    public abstract class Character : MonoBehaviour, IEffectInstigator, IEffectTarget
    {
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private CharacterBehavior _behavior;
        [SerializeField] private CharacterStats _stats;
        [SerializeField] private StatModifierContainer _statModifierContainer;
        [SerializeField] private StatusEffectController _statusEffectController;

        public Movement Movement => _movement;
        public Health Health => _health;
        public Attack Attack => _attack;
        public CharacterBehavior Behavior => _behavior;
        public CharacterStats Stats => _stats;
        public StatModifierContainer StatModifierContainer => _statModifierContainer;
        public StatusEffectController StatusEffectController => _statusEffectController;

        public Vector2 SpawnPosition { get; private set; }
        public Transform Target { get; private set; }

        protected virtual void OnValidate()
        {
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _attack = GetComponent<Attack>();
            _behavior = GetComponent<CharacterBehavior>();
            _stats = GetComponent<CharacterStats>();
            _statModifierContainer = GetComponent<StatModifierContainer>();
            _statusEffectController = GetComponent<StatusEffectController>();
        }

        protected virtual void Awake()
        {
            SpawnPosition = transform.position;
            _behavior.Initialize(this);
            _movement.Initialize(_stats);
            _health.Initialize(_stats);
            _statusEffectController.Initialize(this);
        }

        public virtual void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}