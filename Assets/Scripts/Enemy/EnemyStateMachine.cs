using UnityEngine;
using Game.Enemy;

namespace Game.Enemy
{
    /// <summary>
    /// Controls the enemy's current AI state.
    ///
    /// States determine the enemy's behavior such as:
    /// - Idle
    /// - Patrol
    /// - Chase
    /// - Attack
    /// - Dead
    /// </summary>
    public class EnemyStateMachine : MonoBehaviour
    {
        public EnemyController Controller { get; private set; }

        private EnemyState _currentState;

        /// <summary>
        /// Initializes the state machine and enters the initial state.
        /// </summary>
        public void Initialize(EnemyController controller)
        {
            Controller = controller;

            ChangeState(new IdleState(this));
        }

        private void Update()
        {
            _currentState?.Update();
        }

        /// <summary>
        /// Transitions to a new enemy state.
        /// </summary>
        public void ChangeState(EnemyState nextState)
        {
            _currentState?.Exit();

            _currentState = nextState;

            _currentState.Enter();
        }
    }
}