using UnityEngine;
using Game.Gameplay;

namespace Game.Gameplay.Enemy
{
    /// <summary>
    /// Coordinates the enemy's gameplay components.
    ///
    /// The EnemyController owns the enemy's gameplay components and
    /// initializes the EnemyBrain.
    ///
    /// Gameplay behavior is delegated to the current state.
    /// The controller stores shared data required by states.
    /// </summary>
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Sensor))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Attack))]
    [RequireComponent(typeof(EnemyBrain))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Movement _movement;
        [SerializeField] private Sensor _sensor;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private EnemyBrain _brain;

        public Vector2 SpawnPosition { get; private set; }

        public Movement Movement => _movement;
        public Sensor Sensor => _sensor;
        public Health Health => _health;
        public Attack Attack => _attack;
        public EnemyBrain Brain => _brain;

        public Transform Target { get; private set; }


#if UNITY_EDITOR
        private void OnValidate()
        {
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _sensor = GetComponent<Sensor>();
            _brain = GetComponent<EnemyBrain>();
        }
#endif

        private void Awake()
        {
            SpawnPosition = transform.position;
            _brain.Initialize(this);
        }


        public void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}