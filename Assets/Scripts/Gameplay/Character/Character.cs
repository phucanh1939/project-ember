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
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(StatModifierContainer))]
    [RequireComponent(typeof(StatusEffectController))]
    [RequireComponent(typeof(ProjectileSpawner))]
    public abstract class Character : MonoBehaviour, IEffectInstigator, IEffectTarget
    {
        [Header("Identity")]
        [SerializeField] private EntityFaction _faction;

        [Header("Components")]
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private CharacterBehavior _behavior;
        [SerializeField] private Stats _stats;
        [SerializeField] private StatModifierContainer _statModifierContainer;
        [SerializeField] private StatusEffectController _statusEffectController;
        [SerializeField] private ProjectileSpawner _projectileSpawner;

        private Transform _transform;
        private Vector2 _spawnPosition;
        private Transform _target;

        public EntityFaction Faction => _faction;
        public Movement Movement => _movement;
        public Health Health => _health;
        public Attack Attack => _attack;
        public CharacterBehavior Behavior => _behavior;
        public Stats Stats => _stats;
        public StatModifierContainer StatModifierContainer => _statModifierContainer;
        public StatusEffectController StatusEffectController => _statusEffectController;
        public IProjectileSpawner ProjectileSpawner => _projectileSpawner;
        public IDamageable Damageble => _health;
        public IStatModifierReceiver StatModifierReceiver => _statModifierContainer;
        public IStatusEffectReceiver StatusEffectReceiver => _statusEffectController;
        public IStateInterruptor IStateInterruptor => _behavior.StateInterruptor;
        public Vector2 Position => _transform.position;
        public IStatProvider StatsProvider => _stats;

        public Vector2 SpawnPosition => _spawnPosition;
        public Transform Target => _target;

        protected virtual void OnValidate()
        {
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _attack = GetComponent<Attack>();
            _behavior = GetComponent<CharacterBehavior>();
            _stats = GetComponent<Stats>();
            _statusEffectController = GetComponent<StatusEffectController>();
            _statModifierContainer = GetComponent<StatModifierContainer>();
            _projectileSpawner = GetComponent<ProjectileSpawner>();
        }

        protected virtual void Awake()
        {
            _transform = transform;
            _spawnPosition = transform.position;
            _behavior.Initialize(this);
            _movement.Initialize(_stats);
            _health.Initialize(_stats);
            _statusEffectController.Initialize(this);
            _projectileSpawner.Initialize(this);
        }

        public virtual void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}