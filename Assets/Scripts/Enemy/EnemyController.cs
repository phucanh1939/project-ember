using UnityEngine;
using Game.Gameplay;

namespace Game.Enemy
{
    /// <summary>
    /// Coordinates the enemy's gameplay components.
    ///
    /// The EnemyController owns the enemy's gameplay components and
    /// initializes the EnemyStateMachine.
    ///
    /// Gameplay behavior is delegated to the current state.
    /// </summary>
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyStateMachine))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Movement _movement;
        [SerializeField] private Health _health;
        [SerializeField] private EnemyStateMachine _stateMachine;

        public Movement Movement => _movement;
        public Health Health => _health;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _movement ??= GetComponent<Movement>();
            _health ??= GetComponent<Health>();
            _stateMachine ??= GetComponent<EnemyStateMachine>();
        }
#endif

        private void Awake()
        {
            _stateMachine.Initialize(this);
        }


    }
}