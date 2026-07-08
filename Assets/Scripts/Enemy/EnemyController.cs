using UnityEngine;
using Game.Gameplay;

namespace Game.Enemy
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
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Sensor))]
    [RequireComponent(typeof(EnemyBrain))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private Sensor _sensor;
        [SerializeField] private EnemyBrain _brain;


        [Header("Target Settings")]
        [SerializeField]
        private LayerMask _targetMask;


        public Movement Movement => _movement;
        public Health Health => _health;
        public Sensor Sensor => _sensor;
        public EnemyBrain Brain => _brain;
        public LayerMask TargetMask => _targetMask;

        public Transform Target { get; private set; }


#if UNITY_EDITOR
        private void OnValidate()
        {
            _movement = GetComponent<Movement>();
            _health = GetComponent<Health>();
            _sensor = GetComponent<Sensor>();
            _brain = GetComponent<EnemyBrain>();

            if (_targetMask == 0)
            {
                _targetMask = LayerMask.GetMask("Player");
            }
        }
#endif

        private void Awake()
        {
            _brain.Initialize(this);
        }


        public void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}